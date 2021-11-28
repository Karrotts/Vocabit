import QuizItem from "./quizItem";

export default class Quiz {
    constructor() {
        this.items = [];
    }

    addItem(quizItem) {
        if (quizItem instanceof QuizItem) {
            this.items.push(quizItem);
        } else {
            console.warn("Quiz item is not an instance of QuizItem");
        }
    }
}

