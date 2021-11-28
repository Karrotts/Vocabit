import React from "react";

export default class QuizItemComponent extends React.Component {
    renderOptions(options) {
        const listedOptions = []
        for(let i = 0; i < options.length; i++) {
            listedOptions.push(
							<div className="input-group quiz-item-container">
								<div className="input-group-prepend">
									<div className="input-group-text expand-radio">
										<input 
											type="radio"
											name="answers"
											aria-label={options[i]} 
											checked={i === this.props.item.chosenAnswer}
											onChange={() => this.props.optionChangeHandler(i)}
										/>
									</div>
								</div>
								{options[i]}
							</div>
            )
        }
        return listedOptions;
    }

    render() {
        return (
            <div>
                <h1 className="display-4">{this.props.item.question}</h1>
                <form>
                    {this.renderOptions(this.props.item.answers)}
                </form>
            </div>
        );
    }
}