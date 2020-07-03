var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('sass', () => {
  return gulp.src('Content/scss/**/*.scss')
    .pipe(sass({ outputStyle: 'compressed' })) // Converts Sass to CSS with gulp-sass
    .pipe(gulp.dest('wwwroot/css'))
});