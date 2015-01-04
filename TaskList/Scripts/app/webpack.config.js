
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
        root: ['modules', 'ui']
    },
    module: {
         loaders: [
        //   { test: /\.css/, exclude: /\.usable\.css$/, loader: ExtractTextPlugin.extract("style-loader", "css-loader") },
        //   { test: /\.useable\.css$/, loader: "style/useable!style-loader!css-loader" },
        //   { test: /\.gif/, loader: "url-loader?limit=10000&minetype=image/gif" },
        //   { test: /\.jpg/, loader: "url-loader?limit=10000&minetype=image/jpg" },
        //   { test: /\.png/, loader: "url-loader?limit=10000&minetype=image/png" },
          { test: /\.js$/, loader: "jsx-loader" }
        //   { test: /\.coffee$/, loader: "jsx-loader!coffee-loader" },
        //   { test: /\.less$/, exclude: /\.useable\.less$/, loader: ExtractTextPlugin.extract("style-loader", "css-loader!less-loader") },
        //   { test: /\.useable\.less$/, loader: "style/useable!style-loader!css-loader!less-loader" }
        ],
        noParse: /\.min\.js/
    },
    externals: {
        "jquery": "jQuery"
    }
};