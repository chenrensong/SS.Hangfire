var generate = require('node-chartist');
var https = require('https');

module.exports = function (callback, type, options, data) {

    https.get('https://www.baidu.com/', function (res) {
        console.log("statusCode: ", res.statusCode);
        console.log("headers: ", res.headers);
        res.on('data', function (d) {
            callback(null, res.statusCode);
        });

    }).on('error', function (e) {
        callback(null, "ssss");
    });

    //generate(type, options, data).then(
    //    result => callback(null, result), // Success case
    //    error => callback(error)          // Error case
    //);
};
