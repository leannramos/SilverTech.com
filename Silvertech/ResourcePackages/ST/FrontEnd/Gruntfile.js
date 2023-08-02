module.exports = function (grunt) {
    
    require('load-grunt-tasks')(grunt, {scope: 'devDependencies'});
    
    // Inform that a file was changed
    grunt.event.on('watch', function(action, filepath, target) {
      grunt.log.writeln(target + ': ' + filepath + ' has ' + action);
    });

    // Grunt Init
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'), //read in the package data

        // ------------------------------------------------
        // nunjucks
        // Description: compiles nunjuck templates into html and copies /library
        // ------------------------------------------------
        nunjucks: {
            options: {
                data: 'Frontend Boilerplate With Nunjucks',
                paths: ['dev/App', 'dev/App/MasterPages']
            },
            render: {
                files: [{
                    expand: true,
                    cwd: 'dev/App/Templates',
                    src: [
                        '**/*.html',
                        '!**/_*.html'
                    ],
                    dest: './static_html',
                    ext: '.html'
                }]
            }
        },

        // ------------------------------------------------
        // Express
        // Description: starts up a connect server with live reload
        // ------------------------------------------------
        express: {
            dev: {
                options: {
                    port: 8000,
                    script: 'index.js'
                }
            }
        },

        // ------------------------------------------------
        // Watch
        // Description: watches scss and js source directories for changes
        // ------------------------------------------------
        watch: {
            options: {
                spawn: false,
                reload: true
            },
            css: {
                files: '<%= pkg.path.src.scss %>/**/*.scss',
                tasks: ['css']
            },
            js: {
                files: ['<%= pkg.path.src.js %>/**/*.js'],
                tasks: ['js']
            }
        },

        // ------------------------------------------------
        // Sass
        // Description: compiles .scss files into .css file(s)
        // ------------------------------------------------
        sass: {
            options: {
                sourceMap: true,
                outputStyle: 'expanded',
                implementation: require('node-sass')
            },
            dist: {
                files: {
                    '<%= pkg.path.dest.css %>/core.css': '<%= pkg.path.src.scss %>/core.scss',
					'<%= pkg.path.dest.css %>/designer.css': '<%= pkg.path.src.scss %>/designer.scss',
                    '<%= pkg.path.dest.css %>/app.css': '<%= pkg.path.src.scss %>/app.scss'
                }
            }
        },
        // ------------------------------------------------


        // ------------------------------------------------
        // Autoprefixer
        // Description: add vendor prefixes to the compiled .css file(s)
        // ------------------------------------------------
        autoprefixer: {
            options: {
                browsers: ['last 7 versions'],
                map: true
            },
            core: {
                src: '<%= pkg.path.dest.css %>/core.css',
                dest: '<%= pkg.path.dest.css %>/core.css'
            },
            app: {
                src: '<%= pkg.path.dest.css %>/app.css',
                dest: '<%= pkg.path.dest.css %>/app.css'
            },
            designer: {
                src: '<%= pkg.path.dest.css %>/designer.css',
                dest: '<%= pkg.path.dest.css %>/designer.css'
            }
        },
        // ------------------------------------------------


        // ------------------------------------------------
        // CSS Min
        // Description: minifies .css files
        // ------------------------------------------------
        cssmin: {
            cutup: {
                keepSpecialComments: true,
                expand: true,
                cwd: '<%= pkg.path.dest.css %>',
                src: ['*.css', '!*.min.css'],
                dest: '<%= pkg.path.dest.css %>',
                ext: '.min.css'
            }
        },
        // ------------------------------------------------


        // ------------------------------------------------
        // JS Hint
        // Description: checks JS code for errors
        // ------------------------------------------------
        jshint: {
            all: ['<%= pkg.path.src.js %>/**/*.js'],
            options: {
                esversion: 6
            }
        },
        // --------------------------------------------

        // ------------------------------------------------
        // Rollup
        // Description: Ecma Script 6 Javascript module bundler
        // -
        rollup: {
            main: {
                'dest': 'dev/library/js/mainES6.js',
                'src': 'dev/library/src/js/main/main.js'
            }
        },
        // ------------------------------------------------

        // ------------------------------------------------
        // Babel
        // Description: compiles js files from ES6 using Babel
        // ------------------------------------------------
        babel: {
            options: {
                sourceMap: true,
                presets: ['babel-preset-env']
            },
            dist: {
                files: {
                    'dev/library/js/main.js': 'dev/library/js/mainES6.js'
                }
            }
        },
        // ------------------------------------------------


        // ------------------------------------------------
        // Uglify
        // Description: minifies JS file
        // ------------------------------------------------
        // uglify: {
        //     options: {
        //         mangle: {
        //             except: ['jQuery']
        //         },
        //         preserveComments: 'none'
        //     },
        //     my_target: {
        //         files: {
        //             '<%= pkg.path.dest.js %>/main.js': ['<%= pkg.path.dest.js %>/main.js']
        //         }
        //     }
        // },
        // ------------------------------------------------

        prettify: {
            options: {
                indent: 4
            },
            all: {
                expand: true,
                cwd: './',
                ext: '.html',
                src: ['*.html'],
                dest: './'
            }
        },

        // ------------------------------------------------
        // Copy
        // Description: library to dest
        // ------------------------------------------------
        copy: {
            devToPublic: {
                files: [{ 
                    expand: true, 
                    cwd: "dev/library/", 
                    src: "**", 
                    dest: "public/library/" 
                }]                
            },
            devToStaticHTML: {
                files: [{ 
                    expand: true, 
                    cwd: "dev/library/", 
                    src: "**", 
                    dest: "static_html/library/" 
                }]                
            }
        },
        // ------------------------------------------------

        

        browserSync: {
            dev: {
                bsFiles: {
                    src : [
                        '<%= pkg.path.dest.css %>/*.css', 
                        '<%= pkg.path.dest.js %>/*.js',
                        '<%= pkg.path.src.html %>/**/*.html'
                    ]
                },
                options: {
                    proxy: "http://localhost:8000/",
                    watchTask: true
                }
            }
        },

        

    });

    grunt.loadNpmTasks('grunt-browser-sync');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-autoprefixer');
    grunt.loadNpmTasks('grunt-nunjucks-2-html');
    grunt.loadNpmTasks('grunt-prettify');

    grunt.registerTask('css', ['sass', 'autoprefixer', 'cssmin', 'nunjucks', 'copy']);
    grunt.registerTask('js', ['rollup', 'babel', 'nunjucks', 'copy']);
    grunt.registerTask('html', ['nunjucks', 'css', 'js', 'copy', 'prettify']);
    grunt.registerTask('default', ['css', 'js', 'browserSync', 'express', 'watch']);
};