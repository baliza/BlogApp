
$(function () {
    Post = Backbone.Model.extend({
        defaults: function () {
            return {
                body: null,
                permalink: null,
                author: null,
                title: null,
                permalink: null,
                createdOn: null
            }
        }
    });

    PostSet = Backbone.Collection.extend({
        url: "../api/Post/",
        model: Post,
        newPost: function (post) {
            var url = "../api/Post/";

            console.log('newPost: ' + post.title);
            var self = this;
            $.ajax({
                url:url,
                dataType: "json",
                type: "POST",
                success:function (data) {
                    console.log("search success: " + data.length);
                    self.reset(data); 
                }
            });
        }
      });


});