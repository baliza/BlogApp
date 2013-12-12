

$(function () {

    // Our overall **AppView** is the top-level piece of UI.
    AppView = Backbone.View.extend({

        // Instead of generating a new element, bind to the existing skeleton of
        // the App already present in the HTML.
        el: $("#app"),

        events: {
            "click #addPost": "addPost"
        },

        addPost: function () {
            var item = {
                title: this.newPostTitle.val(),
                author: this.newPostAuthor.val(),
                body: this.newPostBody.val()
            };

            //postsList.create(item);
            postsList.newPost(item);

            this.newPostTitle.val('');
            this.newPostAuthor.val('');
            this.newPostBody.val('');
        },

        initialize: function () {
            this.newPostTitle = this.$("#postTitle");
            this.newPostAuthor = this.$("#postAuthor");
            this.newPostBody = this.$("#postBody");

            this.listenTo(this.model, 'change', this.render);
            this.listenTo(this.model, 'add', this.render);
        },

        render: function () {
            $("#postsList").html("");
            if (this.model.length) {
                for (var i = 0; i < this.model.length; i++) {
                    var view = new PostView({ model: this.model.at(i) });
                    $("#postsList").append(view.render().el);
                }
            }
        }
    });
});