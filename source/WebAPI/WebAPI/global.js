$(function () {

    postsList = new PostSet();

    postsList.fetch({ data: { page: 'no' } });

    var app = new AppView({ model: postsList });
    app.render();
});