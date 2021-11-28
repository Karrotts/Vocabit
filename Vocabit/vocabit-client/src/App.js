import React from 'react';
import Quiz from './models/quiz';
import QuizItem from './models/quizItem';
import QuizItemComponent from './QuizItemComponent';
import QuizData from './questions.json';
import './App.css';

export default class App extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			quiz: GenerateQuiz(),
			currentQuestion: 0,
		}
	}

	renderHeader(currentQuestionNumber) {
		let header = [];
		if (currentQuestionNumber > 0) {
			header.push(
				<div className="col-md-3 justify-content-center justify-content-md-between">
					<button 
						onClick={() =>
							this.setState({currentQuestion: currentQuestionNumber - 1})
						}
						className="btn btn-outline-primary"
					>
						Previous
					</button>
				</div>
			);
		} else {
			header.push(<div className="col-md-3 justify-content-center justify-content-md-between"></div>)
		}

		header.push(
			<div 
				className="col-md-3 justify-content-center justify-content-md-between"
				style={{textAlign: 'center'}}
			>
				Question #{currentQuestionNumber + 1} of {this.state.quiz.items.length}
			</div>
		);

		if (currentQuestionNumber < this.state.quiz.items.length - 1) {
			header.push(
				<div className="col-md-3 justify-content-center justify-content-md-between">
					<button 
						onClick={() => 
							this.setState({currentQuestion: currentQuestionNumber + 1})
						}
						className="btn btn-outline-primary"
						style={{float: 'right'}}
					>
					Next
					</button>
				</div>
			)
		} else {
			header.push(<div className="col-md-3 justify-content-center justify-content-md-between"></div>)
		}
		return header;
	}

	handleNextButton() {
		if (this.state.currentQuestion < this.state.quiz.items.length - 1) {
			this.setState({currentQuestion: this.state.currentQuestion + 1});
		} else {
			// temp grading system, should be replaced with a different rendered component at some point
			const totalItems = this.state.quiz.items.length;
			let correctItems = 0;
			const score = () => ((correctItems / totalItems) * 100) + "%";
			for (let i = 0; i < totalItems; i++) {
				if (this.state.quiz.items[i].chosenAnswer === this.state.quiz.items[i].correctAnswer) {
					correctItems++;
				}
			}
			alert("Results: " + correctItems + " / " + totalItems +
			"\nTotal Score: " + score());
		}
	}

	optionChange(currentQuestion, value) {
		let quizCopy = this.state.quiz;
		quizCopy.items[currentQuestion].chosenAnswer = value;
		this.setState({quiz: quizCopy});
	}

	render() {
		return (
			<div className="container">
				<div className="d-flex flex-wrap align-items-center justify-content-center justify-content-md-between py-3 mb-4 border-bottom">
					{this.renderHeader(this.state.currentQuestion)}
				</div>
				<QuizItemComponent 
					item={this.state.quiz.items[this.state.currentQuestion]} 
					nextHandler={() => this.handleNextButton()}
					optionChangeHandler={(value) => this.optionChange(this.state.currentQuestion, value)}
				/>
				<br />
				<button 
					className={this.state.currentQuestion === this.state.quiz.items.length - 1 ? "btn btn-success" : "btn btn-primary"} 
					onClick={() => this.handleNextButton()}
				>
					{this.state.currentQuestion === this.state.quiz.items.length - 1 ? "Submit" : "Next Question"}
				</button>
			</div>
		)
	}
}

function GenerateQuiz() {
	let items = [];
	for (let i = 0; i < QuizData.questions.length; i++) {
		items.push(new QuizItem(
			QuizData.questions[i].question,
			QuizData.questions[i].answers,
			QuizData.questions[i].correctAnswer
		));
	}
	const quiz = new Quiz();
	quiz.items = items;
	return quiz;
}