const express    = require("express"),
app              = express(),
bodyParser       = require("body-parser"),
mongoose         = require("mongoose");

// Maybe should my own file to setup schemas?

// Set up a student schema

// Set up a teacher schema

// Set up a class Schema
var classSchema = new mongoose.Schema({
    teacher: mongoose.Schema.Types.ObjectId, //This might be changed to an int/id idk 
    teacherAssistants: Array,
    students: Array,
    sessions: Array
});
// sessions schema
var sessions = new mongoose.Schema({
    teacher: mongoose.Schema.Types.ObjectId, //This might be changed to an int/id idk 
    teacherAssistants: Array,
    numberSession: Number,
    description: String,
    class: mongoose.Schema.Types.ObjectId,
    students: Array,
    questions: Array
});

// questions schema
var questions = new moongoes.Schema ({
    question: String,
    student: mongoose.Schema.Types.ObjectId
});

// answers schema

// Set up database
mongoose.connect("mongodb+srv://alexlevinmongo:Infosys123@cluster0-ke9hl.mongodb.net/test?retryWrites=true&w=majority");
app.use(bodyParser.urlencoded({extended: true}));

// This is the homepage
app.get("/", function(req, res){
	res.send("Hi this is a test!");
});

//set up server
app.listen(3000, function() {
    console.log('listening on 3000');
  });