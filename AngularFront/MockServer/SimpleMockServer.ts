//import { Server } from "http";

var http = require('http');
var url = require('url');
var fs = require('fs');
const port = 8082
var server = http.createServer(function(request, response) {
    var path = url.parse(request.url).pathname;
    switch (path) {
        case '/':
            response.writeHead(200, {
                'Content-Type': 'text/plain'
            });
            response.write("This is Test Message.");
            response.end();
            break;
        case '/braResenarHtmlMock':
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
        case '/paymentMock':
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
            case '/3dsecure':
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

            case '/lyckadBokning':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan lyckadBokning.html finns ej :" + error);
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
        case '/avbokad':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan hanterabokningsmock.html finns ej :" + error);
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
            //Efter hantera bokning avboka resa. Sen kommer modal
        case '/avbokningskonfiramtion':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan hanterabokningsmock.html finns ej :" + error);
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
        case '/hanterabokningmock':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan hanterabokningsmock.html finns ej :" + error);
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
        case '/simple':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan simple.html finns ej :" + error);
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

        case '/valjResaMock':
        fs.readFile(__dirname + path + '.html', function(error, data) {
            if (error) {
                response.writeHead(404);
                response.write("Sidan valjResaMock.html finns ej :" + error);
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
        case '/start':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan start.html finns ej :" + error);
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
            case '/frame':
            fs.readFile(__dirname + path + '.html', function(error, data) {
                if (error) {
                    response.writeHead(404);
                    response.write("Sidan frame.html finns ej :" + error);
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
            response.write("opps this doesn't exist - 404");
            response.end();
            break;
    }
});
server.listen(port, (err) => {
    if (err) {
      return console.log('something bad happened', err)
    }

    console.log(`server is listening on ${port}`)
  })