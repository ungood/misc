// A test of processing.js
// Code by Jason Walker: http://onetrue.name
// Base off original by Richard Poole: http://sycora.com/
// Algorithm by Craig Reynolds: http://www.red3d.com/cwr/boids/
// Licence for this file: http://creativecommons.org/licenses/MIT/
dojo.require("dojo._base.Color");
dojo.require("dojox.gfx");
dojo.require("dojox.color");

function vector(x, y) {
	this.x = x;
	this.y = y;

	// Constrains the magnitude of the vector to be between min and max.
	this.constrain = function(min, max) {
		var mag = this.mag();
		if(mag == 0) mag = 0.0000001;

		var constraint = Math.min(max, Math.max(min, mag));
		return this.scalar(constraint / mag);
	}

	this.add = function(that) {
		return new vector(this.x + that.x, this.y + that.y);
	}

	this.sub = function(that) {
		return new vector(this.x - that.x, this.y - that.y);
	}

	this.dot = function(that) {
		return this.x * that.x + this.y * that.y;
	}

	this.mag2 = function() {
		return this.x * this.x + this.y * this.y;
	}

	this.mag = function() {
		return Math.sqrt(this.mag2());
	}

	this.scalar = function(s) {
		return new vector(this.x * s, this.y * s);
	}

	this.normalize = function() {
		var mag = this.mag();
		if(mag == 0)
			return this;

		return this.scalar(1 / mag);
	}
}

vector.create = function(direction, magnitude) {
	return new vector(
		Math.cos(direction) * magnitude,
		Math.sin(direction) * magnitude
	);
}

function BoidController(surface, options) {
	var settings = {
		frameRate: 50,
		background: "#ddf",
		debug: false,
		clipToFrame: true,
		numBoids: 30,
		trailLength: 10,
		visionRadius: 50,
		color: true,
		
		minSpeed: 3,
		maxSpeed: 5,
		
		minAccel: 0,
		maxAccel: 1,	

		randomWeight: 1,
		positionWeight: 2,
		velocityWeight: 4,
		separationWeight: 8,
		targetWeight: 6
	};
		
	var boids = [];
	var intervalHandle = null;

	var target = null;
	var moveTarget = false;
	dojo.connect(surface.node, "onmousemove", function(e) {
		if(moveTarget)
			target = new vector(e.layerX, e.layerY);
	});
	dojo.connect(surface.node, "onmousedown", function(e) {
		moveTarget = true;
	});
	dojo.connect(surface.node, "onmouseup", function() {
		moveTarget = false;
	});

	
	function draw() {
		surface.clear();
		
		surface
			.createRect({
				x: 0, y: 0,
				width: settings.dimensions.width,
				height: settings.dimensions.height})
			.setStroke("black")
			.setFill(settings.bgColor);

		var c = target || settings.center;
		var cfill = dojo.blendColors(settings.bgColor, new dojo.Color("black"), 0.10);
		var width = moveTarget ? 2 : 1;
		surface
			.createCircle({
				cx: c.x,
				cy: c.y,
				r: Math.min(settings.dimensions.width, settings.dimensions.height) / 25
			})
			.setStroke({color: "black", width: width})
			.setFill(cfill)

		for(i in boids) {
			boids[i].draw(surface, settings.bgColor);
			if(settings.debug)
				boids[i].drawDebug(surface);
		}
	}

	function update() {
		for(var i in boids) {
			boids[i].updateNeighbors(boids);
			boids[i].updateStats();
			boids[i].update(target || settings.center);
		}
	}

	this.updateSettings = function(newSettings) {
		settings = dojo.mixin(settings, newSettings);
		
		settings.visionRadius2 = settings.visionRadius * settings.visionRadius;
		settings.interval = 1000 / settings.frameRate;
		settings.dimensions = surface.getDimensions();
		settings.center = new vector(
			settings.dimensions.width / 2,
			settings.dimensions.height / 2);
		settings.bgColor = new dojo.Color(settings.background);
		settings.totalWeight = settings.positionWeight 
			+ settings.velocityWeight 
			+ settings.separationWeight
			+ settings.randomWeight
			+ settings.targetWeight;
		
		this.restart();
	}
		
	this.restart = function() {
		boids = [];
		for(var i = 0; i < settings.numBoids; i++) {
			boids.push(Boid.createRandom(i, settings));
		}
		
		if(intervalHandle)
			clearInterval(intervalHandler);
		intervalHandle = setInterval(function() { update(); draw(); }, settings.interval);
	}

	this.updateSettings(options);
}

function Boid(position, velocity, color, settings) {
	this.position = position;
	this.velocity = velocity;
	this.color = color;
	this.trail = [];
	this.settings = settings;
	
	this.updateNeighbors = function(boids) {
		this.neighbors = [];
		var closestDistance = 999999;
		this.closest = null;
		
		for(var i in boids) {
			if(boids[i] == this) continue;
			
			var dp = boids[i].position.sub(this.position);

			// is boid in sight range?
			var distance2 = dp.mag2();
			if(distance2 > this.settings.visionRadius2) continue;

			if(distance2 < closestDistance) {
				closestDistance = distance2;
				this.closest = boids[i];
			}

			this.neighbors.push(boids[i]);
		}
	}

	this.updateStats = function() {
		if(this.neighbors.length < 1) {
			this.avgPos  = this.position;
			this.avgVel  = this.velocity;
			this.closest = {
				position: this.position
			};
			return;
		}

		var totalPos = new vector(0, 0);
		var totalVel = new vector(0, 0);
		for(var i in this.neighbors) {
			totalPos = totalPos.add(this.neighbors[i].position);
			totalVel = totalVel.add(this.neighbors[i].velocity);
		}

		this.avgPos  = totalPos.scalar(1 / this.neighbors.length);
		this.avgVel  = totalVel.scalar(1 / this.neighbors.length);
	}

	this.update = function(target) {
		var steerRandom = vector.create(
			Math.random() * Math.PI * 2,
			this.settings.randomWeight);
		
		var steerTarget = target
			.sub(this.position)
			.normalize()
			.scalar(this.settings.targetWeight);

		var	steerVelocity = this.avgVel
			.sub(this.velocity)
			.normalize()
			.scalar(this.settings.velocityWeight);
		
		var steerPosition = this.avgPos
			.sub(this.position)
			.normalize()
			.scalar(this.settings.positionWeight);
		
		var steerSeparation = this.position
			.sub(this.closest.position)
			.normalize()
			.scalar(this.settings.separationWeight);
		
		var targetVel = steerVelocity
			.add(steerPosition)
			.add(steerSeparation)
			.add(steerRandom)
			.add(steerTarget)
			.scalar(1 / this.settings.totalWeight);

		var accel = targetVel
			.sub(this.velocity)
			.constrain(this.settings.minAccel, this.settings.maxAccel);

		this.velocity = this.velocity
			.add(accel)
			.constrain(this.settings.minSpeed, this.settings.maxSpeed);
		
		this.position = this.position
			.add(this.velocity);
		
		// if(settings.clipToFrame) {
		// 	this.position
		// }
		
		this.trail.unshift(this.position);
		while(this.trail.length > settings.trailLength)
			this.trail.pop();
	}

	this.draw = function(surface, background) {
		surface.createCircle({
			cx: this.position.x,
			cy: this.position.y,
			r: 2
		}).setFill(this.color);
		
		for(var i = 1; i < this.trail.length; i++) {
			var ratio = i / this.trail.length;
			var blend = dojo.blendColors(this.color, background, ratio);
			surface.createLine({
				x1: this.trail[i-1].x,
				y1: this.trail[i-1].y,
				x2: this.trail[i].x,
				y2: this.trail[i].y
			}).setStroke(blend);
		}
	}
	
	this.drawDebug = function(surface) {
		for(var i in this.neighbors) {
			surface.createLine({
				x1: this.position.x,
				y1: this.position.y,
				x2: this.neighbors[i].position.x,
				y2: this.neighbors[i].position.y})
			.setStroke("black");
		}

		if(this.avgPos) {
			surface.createCircle({
				cx: this.avgPos.x,
				cy: this.avgPos.y,
				r: 2})
			.setStroke(this.color);
		}
	}
}

Boid.createRandom = function(i, settings) {
	var position = new vector(
		Math.random() * settings.dimensions.width,
		Math.random() * settings.dimensions.height);
		
	var velocity = vector.create(
		Math.random() * Math.PI * 2,
		settings.minSpeed);
	color = new dojo.Color("black");
	if(settings.color) {
		color = dojox.color.fromHsl(
			(360 / settings.numBoids) * i,
			100,
			50);
	}

	return new Boid(position, velocity, color, settings);
}