import React, { useEffect, useState } from "react";
import "./Editor.css";
import axios from "axios";
import { Prism as SyntaxHighlighter } from "react-syntax-highlighter";
import { darcula } from "react-syntax-highlighter/dist/esm/styles/prism";


const Editor = ({ folder }) => {
  const [code, setCode] = useState("print(1)");
  const [response, setResponse] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();
    axios
      .post("http://localhost:5054/api/My")
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  const handleRun = async (event) => {
    event.preventDefault();
    uploadFile();
    axios
      .post(`http://localhost:5054/api/RunCode`)
      .then(function (response) {
        console.log(response); 
        setResponse(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  const uploadFile = async () => {
    const codedata = new FormData();
    codedata.append("code", code);
    console.log(codedata);
    await axios
      .post("http://localhost:5054/api/Code", codedata, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  };
  const handleInputChange = (event) => {
    setCode(event.target.value);
  };
  const textarea = document.querySelector("textarea");

  const lineNumbers = document.querySelector(".line-numbers");

  function updateLineNumbers() {
    const lines = textarea.value.split("\n");
    const lineNumbersHTML = lines
      .map((line, index) => `<div>${index + 1}</div>`)
      .join("");
    lineNumbers.innerHTML = lineNumbersHTML;
  }

  // textarea.addEventListener('input', updateLineNumbers);
  // updateLineNumbers();

  return (
    <div className="folder-tree">
      <div class="editor-container">
        <div class="editor-header">AplLit Code Editor</div>
        <div class="editor-body">
          <div class="editor-sidebar">
            <ul class="editor-sidebar-list">
              <li class="editor-sidebar-list-item active">app.js</li>
              <li class="editor-sidebar-list-item">styles.css</li>
              <li class="editor-sidebar-list-item">index.html</li>
            </ul>
          </div>
          <div class="editor-content">
            <div class="editor-code">
              <div className="code-editor">
                <div class="line-numbers"></div>
                <textarea value={code} onChange={handleInputChange}></textarea>
                <form onSubmit={handleRun}>
                  <input type="submit" value="Run" />
                </form>
              </div>
            </div>
            <div class="editor-console">
              <div class="console-header">
                Console
                <form onSubmit={handleSubmit}>
                  <input type="submit" value="Send data" />
                </form>
              </div>

              <div class="console-body">
              <SyntaxHighlighter language="python" style={darcula}>
                {response}
              </SyntaxHighlighter>
{/* 
                <div>
                  <p> {response} </p>
                </div> */}
              </div>
              <div class="console-input">
                <input type="text" placeholder="Type your command here" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Editor;
