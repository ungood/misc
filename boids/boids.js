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
		frameRate: 25,
		numBoids: 30,
		trailLength: 10,
		minSpeed: 3,
		maxSpeed: 5,
		visionRadius: 50,
		background: "#eee"
	};
	var settings = dojo.extend(defaults, options);
	var dimensions = surface.getDimensions();
	var center = new vector(dimensions.width / 2, dimensions.height / 2);
	var boids = [];

	for(var i = 0; i < settings.numBoids; i++) {
		boids.push(Boid.createRandom(i, dimensions, settings));
		console.log(boids[i]);
	}

	function draw() {
		surface.clear();
		var bgColor = new dojo.Color(settings.background);
		surface.createRect({
			x: 0, y: 0,
			width: dimensions.width,
			height: dimensions.height
		}).setStroke("black").setFill(bgColor);

		for(i in boids) {
			boids[i].findNeighbors(surface, boids, settings);
			boids[i].draw(surface, bgColor);
		}
	}

	function update() {
		for(i in boids) {
			var accel = center.sub(boids[i].position).scalar(.005);
			var rand = vector.create(Math.random() * Math.PI * 2, 1 - Math.random() * 2);
			boids[i].update(accel.add(accel).add(rand));
		}
	}

	setInterval(function() { update(); draw(); }, 1000 / settings.frameRate);
}

function Boid(position, velocity, color, settings) {
	this.position = position;
	this.velocity = velocity;
	this.color = color;
	this.trail = [];

	this.update = function(accel) {
		this.velocity = this.velocity.add(accel);
		this.position = this.position.add(this.velocity);

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

	this.findNeighbors = function(surface, boids, settings) {
		var neighbors = [];
		var vision2 = settings.visionRadius * settings.visionRadius;

		for(var i in boids) {
			if(boids[i] == this) continue;

			var dp = boids[i].position.sub(this.position);

			// is boid in sight range?
			var distance2 = dp.mag2();
			if(distance2 > vision2) continue;

			//var angle = this.velocity.cosAngle(dp);
			surface.createLine({
				x1: this.position.x,
				y1: this.position.y,
				x2: boids[i].position.x,
				y2: boids[i].position.y}).setStroke("black");
		}
	}
}

Boid.createRandom = function(i, dimensions, settings) {
	var position = new vector(
		Math.random() * dimensions.width,
		Math.random() * dimensions.height);
	var velocity = vector.create(
		Math.random() * Math.PI * 2,
		settings.minSpeed + Math.random(settings.maxSpeed - settings.minSpeed));
	var color = dojox.color.fromHsl(
		(360 / settings.numBoids) * i,
		100,
		50);

	return new Boid(position, velocity, color, settings);
}