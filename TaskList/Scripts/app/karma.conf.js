var rewirePlugin = require('rewire-webpack'),
	webpack = require('webpack');

module.exports = function (config) {
		
	var specFiles = './!(node_modules)/**/*.spec.js',
		// list of files / patterns to load in the browser
		files = [
			//'../Tests/lib/phontomjs-shims.js',
			'../jquery-2.1.3.js',
			'test/mock-ajax.js',
			'test/jasmine-jquery.js',
			specFiles
		],
		preprocessors = {};

	// list of preprocessors
	preprocessors[specFiles] = ['webpack'];

	// http://karma-runner.github.io/0.12/config/configuration-file.html
	config.set({

		// frameworks to use
		frameworks: ['jasmine'],

		files: files,

		preprocessors: preprocessors,

		client: {
			// ignore window.console output
			captureConsole: false
		},

		// build for spec files that are loaded by karma
		// http://webpack.github.io/docs/configuration.html
		webpack: {
			cache: true,
			resolve: {
				// Tell webpack to look for required files in bower and node
				modulesDirectories: ['node_modules', '']
			},
			module: {
				loaders: [
				  { test: /\.js$/, loader: 'jsx-loader' },
				  { test: /\.coffee$/, loader: 'null-loader' },
				  { test: /\.css/, loader: 'null-loader' },
				  { test: /\.less$/, loader: 'null-loader' }
				],
				noParse: [/\.less/, /\.coffee/, /\.css/, /\.gif/, /\.jpg/, /\.png/]
			},
			plugins: [
				new rewirePlugin(),
				new webpack.ProvidePlugin({
					jasmineReact: __dirname + '/node_modules/jasmine-react-helpers/src/jasmine-react.js'
				})
			],
			externals: {
				jquery: 'jQuery'
			},
		},

		reporters: ['spec'],

		// web server port
		port: 8081,

		// browser to launch (must be installed)
		browsers: ['devchrome'],

		// custom flags for chrome
		customLaunchers: {
			devchrome: {
				base: 'Chrome',
				// http://peter.sh/experiments/chromium-command-line-switches/
				flags: [
					'--start-maximized',
					'--disable-application-cache',
					'--incognito'
				]
			},
			devphantomjs: {
				base: 'PhantomJS',
				flags: [
					'--remote-debugger-port=9000',
					'--remote-debugger-autorun=yes'
				]
			}
		},

		captureTimeout: 60000,
		browserNoActivityTimeout: 60000,

		// Continuous Integration mode
		// if true, it capture browsers, run tests and exit
		singleRun: false,

		plugins: [
			require('karma-jasmine'),
			require('karma-webpack'),
			require('karma-chrome-launcher'),
			require("karma-phantomjs-launcher"),
			require('karma-spec-reporter')
		]

	});

};