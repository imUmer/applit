
import React, {useEffect, useState } from "react";
import './Editor.css';

const Editor = ({ folder }) => {  
      const [code, setCode] = useState("// Type your code here");
      const [data, setData] = useState([]);

      // useEffect(() => {
      //   const fetchData = async () => {
      //   fetch('http://localhost:5054/My?p=print(1)')
      //     .then(response => response.text())
      //     .then(data => setData(data))
      //     .catch(error => console.error(error));
      //     console.warn(data)};
      // }, []);


      const [response, setResponse] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    fetch("http://localhost:5054/My?p=print(112)")
    .then((result) => {
      result.json().then((resp )=>{
          setResponse(resp);
      });
    });
    // const data = { p: 'print(1)' };
    // const options = {
    //   method: 'POST',
    //   mode: 'no-cors',
      
    //   headers: {
    //     origin: "*",
    //     crossDomain: true,
    //     "Content-Type" : "application/json",
    //     "Access-Control-Allow-Headers": "*",
    //     "Access-Control-Allow-Origin": "*",
    //     "Access-Control-Allow-Methods": "*"    
    //   },
    //   body: JSON.stringify(data)
    // };
    // const res = await fetch('http://localhost:5054/My',options);
    // const json = await res.json();
  //   fetch('http://localhost:5054/My', {
  //     method: 'POST',
  //     mode: 'no-cors',
  //     credentials: 'same-origin',
  //     headers: {
  //         'Accept': 'application/json',
  //         'Content-Type': 'application/json',
  //             "Access-Control-Allow-Headers": "*",
  //             "Access-Control-Allow-Origin": "*",
  //             "Access-Control-Allow-Methods": "*"   
  //     },
  //     body: JSON.stringify({
  //         p: 'print(1)'
  //     })
  // }).then(function (response) {
  //     console.log(response);
  //     return response.json();
  
  // }).catch(function (err) {
  //     // console.log(err)
  // });
    // setResponse(json); 
  }

  const [dataa, setDataa] = useState([]);

  useEffect(() => {
    const fetchData = async () => { 
      const response = await fetch('http://localhost:5054/My?p=print(1)');
      const jsonData = await response.json();
      setDataa(jsonData);
      console.log(dataa);
    };

    fetchData();
  }, []);


      const handleInputChange = (event) => { 
        setCode(event.target.value);
      };
      const textarea = document.querySelector('textarea');
        const lineNumbers = document.querySelector('.line-numbers');

        function updateLineNumbers() { 
        const lines = textarea.value.split('\n');
        const lineNumbersHTML = lines.map((line, index) => `<div>${index + 1}</div>`).join('');
        lineNumbers.innerHTML = lineNumbersHTML;
        }

        // textarea.addEventListener('input', updateLineNumbers);
        // updateLineNumbers();


      return (
        
      <div className="folder-tree">
      <div class="editor-container">
  <div class="editor-header">
    Code Editor
  </div>
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
        </div>
      </div>
      <div class="editor-console">
        <div class="console-header">Console</div>
        <form onSubmit={handleSubmit}>
        <input type="submit" value="Send data" />
      </form>
      <p>Response : {response}</p>
        <div class="console-body">

        <div>
          {dataa.map(item => (
            <div key={item.id}>{item.name}</div>
          ))}
        </div> 
       
        </div>
        <div class="console-input">
          <input type="text" placeholder="Type your command here"/>
        </div>
      </div>
    </div>
  </div>
</div>

      </div>
    );
  };
 
export default Editor;
