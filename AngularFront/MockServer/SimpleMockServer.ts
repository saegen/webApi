var http = require('http');
var url = require('url');
var fs = require('fs');
const port = 8082
var server = http.createServer(function(request, response) {
    var path = url.parse(request.url).pathname;
    switch (path.toLowerCase()) {
        case '/':
            response.writeHead(200, {
                'Content-Type': 'text/plain'
            });
            response.write("This is Test Message from SimpleMockServer.");
            response.end();
            break;
            case '/js/userjs.js':
                fs.readFile(__dirname + path, function(error, data) {
                    if (error) {
                        response.writeHead(404);
                        response.write(error);
                        response.end();
                    } else {
                        response.writeHead(200, {
                            'Content-Type': 'text/javascript'
                        });
                        response.write(data);
                        response.end();
                    }
                });
                break;

            case '/css/mockcss.css':
                fs.readFile(__dirname + path, function(error, data) {
                    if (error) {
                        response.writeHead(404);
                        response.write(error);
                        response.end();
                    } else {
                        response.writeHead(200, {
                            'Content-Type': 'text/css'
                        });
                        response.write(data);
                        response.end();
                    }
                });
                break;
            case '/usermock':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write(error);
                    response.end();
                } else {
                    response.writeHead(200, {
                        'Content-Type': 'text/html'
                    });
                    response.write(data);
                    response.end();
                }
            });
            break;
        case '/submock':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan paymentMock.html finns ej :" + error);
                    response.end();
                } else {
                    response.writeHead(200, {
                        'Content-Type': 'text/html'
                    });
                    response.write(data);
                    response.end();
                }
            });
            break;
            // 3dsecure
            case '/adminmock':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan 3dsecure.html finns ej :" + error);
                    response.end();
                } else {
                    response.writeHead(200, {
                        'Content-Type': 'text/html'
                    });
                    response.write(data);
                    response.end();
                }
            });
            break;
        default:
            response.writeHead(404);
            response.write("SimpleMockServer: opps this doesn't exist - 404");
            response.write("Dir: " + __dirname + ' ,Path ' + path)
            response.end();
            break;
    }
});
server.listen(port, (err) => {
    if (err) {
      return console.log('SimpleMockServer: something bad happened', err)
    }
    console.log(__dirname + '\\mockcss.css');

    console.log(`SimpleMockServer is listening on ${port}`)
  })