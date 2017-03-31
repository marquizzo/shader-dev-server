var webpack = require("webpack");
var path = require("path");

module.exports = {
	// Declare entry points, where you "enter" the script 
	entry:{
		script: "./js/entry.js",
	},

	// Where to output the compiled JS
	output:{
		filename: "[name].js",
		path: __dirname + "/js/",
	},

	module:{
		rules: [
			// any file ending in glsl, vs, fs will use shader-loader to compile
            { test: /\.(glsl|vs|fs)$/, loader: "shader-loader" },
			// any file ending in .js (not in node_modules) will use babel-loader to compile
            { test: /\.js?$/, exclude: [/node_modules/], loader: "babel-loader" },
		]
	},

	resolve: {
        extensions: [".webpack.js", ".web.js", ".js"]
	},

	// Enables dev server to be accessed by computers in local network
	devServer: {
		host: "0.0.0.0",
		port: 8080,
		publicPath: "/js/"
	}
}