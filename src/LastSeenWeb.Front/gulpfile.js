"use strict";

var gulp = require("gulp"),
	concat = require("gulp-concat"),
	cssmin = require("gulp-cssmin"),
	htmlmin = require("gulp-htmlmin"),
	uglifyes = require('uglify-es'),
	composer = require('gulp-uglify/composer'),
	uglify = composer(uglifyes, console),
	merge = require("merge-stream"),
	del = require("del"),
	bundleconfig = require("./bundleconfig.json");

var regex = {
	css: /\.css$/,
	html: /\.(html|htm)$/,
	js: /\.js$/
};

gulp.task("min", ["min:js", "min:css", "min:html"]);

gulp.task("min:js", function () {
	const tasks = getBundles(regex.js).map(function (bundle) {
		return gulp.src(bundle.inputFiles, { base: "." })
			.pipe(concat(bundle.outputFileName))
			.pipe(uglify())
			.pipe(gulp.dest("."));
	});
	return merge(tasks);
});

gulp.task("min:css", function () {
	const tasks = getBundles(regex.css).map(function (bundle) {
		return gulp.src(bundle.inputFiles, { base: "." })
			.pipe(concat(bundle.outputFileName))
			.pipe(cssmin())
			.pipe(gulp.dest("."));
	});
	return merge(tasks);
});

gulp.task("min:html", function () {
	const tasks = getBundles(regex.html).map(function (bundle) {
		return gulp.src(bundle.inputFiles, { base: "." })
			.pipe(concat(bundle.outputFileName))
			.pipe(htmlmin({ collapseWhitespace: true, minifyCSS: true, minifyJS: true }))
			.pipe(gulp.dest("."));
	});
	return merge(tasks);
});

gulp.task("clean", function () {
	const files = bundleconfig.map(function (bundle) {
		return bundle.outputFileName;
	});

	return del(files);
});

gulp.task("watch", function () {
	getBundles(regex.js).forEach(function (bundle) {
		gulp.watch(bundle.inputFiles, ["min:js"]);
	});

	getBundles(regex.css).forEach(function (bundle) {
		gulp.watch(bundle.inputFiles, ["min:css"]);
	});

	getBundles(regex.html).forEach(function (bundle) {
		gulp.watch(bundle.inputFiles, ["min:html"]);
	});
});

function getBundles(regexPattern) {
	return bundleconfig.filter(function (bundle) {
		return regexPattern.test(bundle.outputFileName);
	});
}
