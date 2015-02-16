
var webpack = require("webpack");
module.exports = {
    // This is the main file that should include all other JS files

    target: "web",
    debug: true,
    devtool: "inline-source-map",
    watch: false,
    entry: "./task",
    output: {
        path: "./build",
        publicPath: "/app/build/",
        filename: "build.js",
        libraryTarget: "var",
        library: "Task"
    },
    resolve: {
        // Tell webpack to look for required files in bower and node
        modulesDirectories: ['node_modules'],
        //TODO Need to fix this, is look in all directories right now
        root: ['modules', 'ui']
    },
    module: {
         loaders: [
            { test: /\.css/, exclude: /\.usable\.css$/, loader: "style-loader!css-loader" },
            { test: /\.js$/, loader: "jsx-loader" },   
            { test: /\.less$/, exclude: /\.useable\.less$/, loader: "style-loader!css-loader!less-loader" }
        ],
        noParse: /\.min\.js/
    },
    //Expose jQuery since it isn't part of node modules
    externals: {
        "jquery": "jQuery"
    }
};