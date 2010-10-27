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

	this.cosAngle = function(that) {
		return this.dot(that) / this.mag() * that.mag();
	}

	this.scalar = function(s) {
		return new vector(this.x * s, this.y * s);
	}
}

vector.create = function(direction, magnitude) {
	return new vector(
		Math.cos(direction) * magnitude,
		Math.sin(direction) * magnitude
	);
}

function BoidController(surface, options) {
	var defaults = {
		frameRate: 50,
		background: "#eee",
		debug: true,
		clipToFrame: true,
		numBoids: 30,
		trailLength: 10,
		visionRadius: 50,		
	};
	var settings = defaults;
		
	var boids = [];
	var intervalHandle = null;
	
	function draw() {
		surface.clear();
		
		surface
			.createRect({
				x: 0, y: 0,
				width: settings.dimensions.width,
				height: settings.dimensions.height})
			.setStroke("black")
			.setFill(settings.bgColor);

		for(i in boids) {
			boids[i].draw(surface, settings.bgColor);
			if(settings.debug)
				boids[i].drawNeighbors(surface);
		}
	}

	function update() {
		for(var i in boids) {
			boids[i].updateNeighbors(boids);
			
			
			var accel = settings.center.sub(boids[i].position).scalar(.005);
			var rand = vector.create(Math.random() * Math.PI * 2, 1 - Math.random() * 2);
			boids[i].update(accel.add(accel).add(rand));
		}
	}

	this.updateSettings = function(newSettings) {
		settings = dojo.extend(settings, newSettings);
		settings.visionRadius2 = settings.visionRadius * settings.visionRadius;
		settings.interval = 1000 / settings.frameRate;
		settings.dimensions = surface.getDimensions();
		settings.center = new vector(
			settings.dimensions.width / 2,
			settings.dimensions.height / 2);
			
		settings.bgColor = new dojo.Color(settings.background);
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
		
		for(var i in boids) {
			if(boids[i] == this) continue;
			
			var dp = boids[i].position.sub(this.position);

			// is boid in sight range?
			var distance2 = dp.mag2();
			if(distance2 > this.settings.visionRadius2) continue;

			this.neighbors.push(boids[i]);
		}
	}

	this.update = function(accel) {
		this.velocity = this.velocity.add(accel);
		this.position = this.position.add(this.velocity);
		
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
	
	this.drawNeighbors = function(surface) {
		for(var i in this.neighbors) {
			surface.createLine({
				x1: this.position.x,
				y1: this.position.y,
				x2: this.neighbors[i].position.x,
				y2: this.neighbors[i].position.y}).setStroke("black");
		}
	}
}

Boid.createRandom = function(i, settings) {
	var position = new vector(
		Math.random() * settings.dimensions.width,
		Math.random() * settings.dimensions.height);
		
	var velocity = new vector(0, 0);
	var color = dojox.color.fromHsl(
		(360 / settings.numBoids) * i,
		100,
		50);

	return new Boid(position, velocity, color, settings);
}