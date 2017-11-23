/// <binding ProjectOpened='watch-less, fonts, less-to-css' />
/// <vs AfterBuild='less-to-css' />
var gulp = require('gulp');
var debug = require('gulp-debug');
var onlyChangedFiles = require('gulp-changed');
var less = require('gulp-less');
var path = require('path');
var concat = require('gulp-concat');
var cssnano = require('gulp-cssnano');
var sourcemaps = require('gulp-sourcemaps');
var uglify = require('gulp-uglify');
var xmlpoke = require('gulp-xmlpoke');
var rimraf = require('gulp-rimraf');
var rename = require('gulp-rename');
var sass = require('gulp-sass');


// Add file watchers

gulp.task("watch", function () {
    gulp.watch("./wwwroot/Styles/**/*.scss", ["build:sass"]);
    // Other watchers
});

// Build sass files

gulp.task("build:sass", function () {
    return gulp.src("./wwwroot/Styles/**/*.scss")
        .pipe(sourcemaps.init())
        .pipe(sass().on('error', function (err) {
            console.log(err.message);
            this.emit("end");
        }))
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest("./wwwroot/Styles"));
});

// prepare LESS files

var layouts = ["frontend", "frontend-modal"];

// Build less
layouts.forEach(function (layout) {
    gulp.task("build:less:" + layout, function () {
        return gulp.src('./Styles/' + layout + '/styles.less')
            .pipe(sourcemaps.init())
            .pipe(less({ sourcemap: true }).on('error', function (err) {
                console.log(err.message);
                this.emit("end");
            }))
            //.pipe(autoprefixer(
            //    {
            //        browsers: ['last 2 versions'],
            //        cascade: false
            //    }))
            .pipe(sourcemaps.write("./"))
            .pipe(gulp.dest('./Styles/' + layout + '/'));
    });
});

gulp.task("build:less", layouts.map(function (layout) { return "build:less:" + layout; }));

gulp.task('watch:less', ['build:less'], function () {
    gulp.watch("./Styles/**/*.less", ['build:less']);
});

//Convert less to min
layouts.forEach(function (layout) {

    gulp.task('less-to-css-min:' + layout, function () {
        return gulp
            .src('./Styles/' + layout + '/styles.less')
            .pipe(sourcemaps.init())
            .pipe(less())
            .pipe(cssnano())
            .pipe(rename(layout + ".min.css"))
            .pipe(gulp.dest('./public/css'));
    });
});

gulp.task("less-to-css-min", layouts.map(function (layout) { return "less-to-css-min:" + layout; }));


// transform web.config
function updateDebugMode(mode) {
    return gulp
        .src('./Web.config')
        .pipe(xmlpoke({
        replacements: [{
                xpath: "//system.web/compilation/@debug",
                value: mode
            }]
    }))
        .pipe(gulp.dest('./'));
}
gulp.task('enable-debug-mode', function () { return updateDebugMode("true"); });
gulp.task('disable-debug-mode', function () { return updateDebugMode("false"); });
// prepare JS files
function getScriptFiles() {
    var content = require('fs').readFileSync('./ScriptReferences.json', 'utf8');
    // strip BOM
    content = content.replace(/^\uFEFF/, '');
    var files = JSON.parse(content);
    // make paths relative to gulp
    files = files.map(function (f) { return '.' + f; });
    return files;
}
gulp.task('uglify-js', ['clean-destination-js'], function () {
    return gulp.src(getScriptFiles())
        .pipe(debug({ title: 'js:' }))
        .pipe(sourcemaps.init())
        .pipe(concat('app-min.js'))
        .pipe(gulp.dest('./public/js'))
        .pipe(uglify())
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest('./public/js'));
});
// prepare modernizr separately
// NOTE: make sure modernizr.js is built and placed in the /bower_components/modernizr/ folder
gulp.task('uglify-modernizr', function () {
    return gulp
        .src('./bower_components/modernizr/modernizr.js')
        .pipe(debug({ title: 'js:' }))
        .pipe(sourcemaps.init())
        .pipe(uglify())
        .pipe(sourcemaps.write("./"))
        .pipe(gulp.dest('./public/js'));
});
// copy font files
gulp.task('fonts', function () {
    return gulp
        .src(['./bower_components/bootstrap/dist/fonts/*.*', './bower_components/fontawesome/fonts/*.*'])
        .pipe(onlyChangedFiles('./public/fonts'))
        .pipe(debug({ title: 'font:' }))
        .pipe(gulp.dest('./public/fonts'));
});
// cleanup
gulp.task('clean-destination-js', function () {
    return gulp
        .src([
        './public/js/*.js',
        './public/js/*.js.map'
    ], { read: false })
        .pipe(rimraf({ force: true }));
});
gulp.task('clean-css', function () {
    return gulp
        .src([
        './public/css/*.css',
        './public/css/*.css.map',
        './Styles/*.css',
        './Styles/*.css.map',
        './Styles/Imports/*.css',
        './Styles/Imports/*.css.map'
    ], { read: false })
        .pipe(rimraf({ force: true }));
});
gulp.task('clean-fonts', function () {
    return gulp
        .src(['./public/fonts/*.eot',
        './public/fonts/*.svg',
        './public/fonts/*.ttf',
        './public/fonts/*.woff',
        './public/fonts/*.woff2'], { read: false })
        .pipe(rimraf({ force: true }));
});
gulp.task('prepare-release', ['less-to-css-min', 'fonts', 'uglify-js', 'uglify-modernizr']);
gulp.task('Clean', ['clean-destination-js', 'clean-css', 'clean-fonts']);
//# sourceMappingURL=gulpfile.js.map