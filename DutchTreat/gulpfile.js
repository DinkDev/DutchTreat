/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('clean', function () {
    // place code for your default task here
});

var paths = {
    npm: './node_modules/',
    webroot: './wwwroot/',
    bower: './bower_components/',
    lib: './wwwroot/lib/'
};

gulp.task('copy', ['clean'], function () {
    var npm = {
        'bootstrap': 'bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}',
        'jquery': 'jquery/dist/jquery*.{js,map}',
        'jquery-validation': 'jquery-validation/dist/*.js',
        'jquery-validation-unobtrusive': 'jquery-validation-unobtrusive/*.js'
    }

    for (var destinationDir in npm) {
        gulp.src(paths.npm + npm[destinationDir])
            .pipe(gulp.dest(paths.lib + destinationDir));
    }
});