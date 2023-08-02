const   express =   require('express'),
        nunjucks =  require('nunjucks'),
	    app =       express(),
        path =      require("path");
        router =    express.Router(),
        port =      Number(process.env.PORT || 8000),
        host =      '0.0.0.0';

// Nunjucks Init
nunjucks.configure('dev', {
    autoescape: true,
    express: app,
    watch:true
});


// Allow express to render static files (css/js/img/svgs/etc)
app.use(router);
// set static directories
// app.use(express.static('dev'));
app.use(express.static(path.join(__dirname, 'dev')));

// Allow all possible methods
router.all('/', (req, res, next) => {
    next();
});

//route any name given after /
router.get('/:name?', (req, res) => {
    let view = 'index';

    if (req.params.name != null && req.params.name != "") {
        view = `App/Templates/${req.params.name}`;
    } else {
        view = `App/Templates/index.html`;
    }

    res.render(view);
});

// Listen
const server = app.listen(port, host, () => {
    console.log(`Listening on: http://localhost:${port}`);
});