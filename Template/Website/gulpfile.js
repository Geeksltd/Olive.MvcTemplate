/// <binding ProjectOpened='watch' />
var gulp, debug, sourcemaps, sass;
initialize();

function doRequire(service) {
    try {
        return require(service);
    } catch (error) {
        console.error("ERROR: Failed to load '" + service + "'. Try updating your npm by running ->  npm -g install npm.");
        console.error(error.message);
        throw error;
    }
}

function initialize() {
    gulp = doRequire('gulp');
    debug = doRequire('gulp-debug');    
    sourcemaps = doRequire('gulp-sourcemaps');
    sass = doRequire('gulp-sass');
}

// Add file watchers
gulp.task("watch", function () {
    gulp.watch("./wwwroot/Styles/**/*.scss", ["build-sass"]);
});

// Build sass files
gulp.task("build-sass", function () {
    console.log("** building sass files...");
    return gulp.src("./wwwroot/Styles/*.scss")
        .pipe(sourcemaps.init())
        .pipe(sass().on('error', function (err) {
            console.log(err.message);
            this.emit("end");
        }))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest("./wwwroot/public/css"));
});

gulp.task('minify-css', function () {
	var cssnano = doRequire('gulp-cssnano');
    return gulp
        .src('./wwwroot/public/css/*.css')
        .pipe(cssnano())
        .pipe(gulp.dest('./wwwroot/public/css'));
});


// gulp.task('uglify-js', ['clean-destination-js'], function () {
    // return gulp.src(getScriptFiles())
        // .pipe(debug({ title: 'js:' }))
        // .pipe(sourcemaps.init())
        // .pipe(concat('app-min.js'))
        // .pipe(gulp.dest('.wwwroot/public/js'))
        // .pipe(uglify())
        // .pipe(sourcemaps.write("./"))
        // .pipe(gulp.dest('./wwwroot/public/js'));
// });
// function getScriptFiles() {
    // var content = require('fs').readFileSync('./ScriptReferences.json', 'utf8');
    // // strip BOM
    // content = content.replace(/^\uFEFF/, '');
    // var files = JSON.parse(content);
    // // make paths relative to gulp
    // files = files.map(function (f) { return '.' + f; });
    // return files;
// }


//# sourceMappingURL=gulpfile.js.map