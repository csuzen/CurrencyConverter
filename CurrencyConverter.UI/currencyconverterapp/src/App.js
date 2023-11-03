import logo from './logo.svg';
import './App.css';
import { Component } from 'react';

class App extends Component {

  constructor(props) {
    super(props);

    this.state = {
      result: ""
    }
  }

  API_URL = "http://localhost:5256/";

  async getWord() {
    var input = document.getElementById("currencyInput").value;

    if (!input) {
      alert("Input required");
    }
    else {
      fetch(this.API_URL + "currency/" + input).then(response => response.text())
        .then(data => {
          console.log(data);
          this.setState({ result: data });
        })
    }
  }

  render() {
    const { result } = this.state;
    return (
      <div className="App">
        <h2>Convert Currency Into Words</h2>

        <input id="currencyInput"></input>
        <button onClick={() => this.getWord()}>Convert</button>
        <br />
        <p>{result}</p>
      </div>
    );
  }
}

export default App;
