BlogApp
=======

This is a project to learn MVC5, WepAPI, Mongo, TDD and some other Web tegchnologies (Backbone, KnockoutJS,...)

Using exercices from  Mongo for NodeJs


RestFull Operations


// The main page of the blog
============================
get, '/'



// The main page of the blog, filtered by tag

get, '/tag/:tag'


// A single post, which can be commented on

get, "/post/:permalink"

post, '/newcomment'

get, "/post_not_found"



// Displays the form allowing a user to add a new post. Only works for logged in users

get, '/newpost'

post, '/newpost'



// Login form

get, '/login'

post, '/login'



// Logout page

get, '/logout'


// Welcome page

get, "/welcome"


// Signup form

get, '/signup'

pos, '/signup'



