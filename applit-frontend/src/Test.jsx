import React, { useState } from "react";
import "./App.css";

// sample data for folder tree
const folderData = [
  {
    name: "src",
    type: "folder",
    children: [
      { name: "index.js", type: "file" },
      { name: "app.js", type: "file" },
      {
        name: "components",
        type: "folder",
        children: [
          { name: "Header.js", type: "file" },
          { name: "Footer.js", type: "file" },
        ],
      },
    ],
  },
];

function App() {
  const [consoleMsg, setConsoleMsg] = useState(""); // state for console message
  const [selectedFile, setSelectedFile] = useState(null); // state for selected file in folder tree
  const [code, setCode] = useState(""); // state for code in code editor

  // function to handle file selection from folder tree
  const handleFileSelect = (file) => {
    if (file.type === "file") {
      setSelectedFile(file);
    }
  };

  // function to handle code submission
  const handleSubmit = (e) => {
    e.preventDefault();
    setConsoleMsg("Code submitted!");
  };

  return (
    <div className="App">
      <div className="sidebar">
        <div className="folder-tree">
          <h3>Files</h3>
          <FolderTree data={folderData} onSelect={handleFileSelect} />
        </div>
      </div>
      <div className="main">
        <div className="code-editor">
          <div className="editor-header">
            <h3>{selectedFile ? selectedFile.name : "Untitled"}</h3>
          </div>
          <div className="editor-body">
            <CodeEditor
              code={code}
              setCode={setCode}
              fileName={selectedFile ? selectedFile.name : "Untitled"}
            />
          </div>
          <div className="editor-footer">
            <button onClick={handleSubmit}>Run Code</button>
          </div>
        </div>
        <div className="console">
          <h3>Console</h3>
          {/* <Console message={consoleMsg} /> */}
        </div>
      </div>
    </div>
  );
}

// FolderTree component
function FolderTree({ data, onSelect }) {
  return (
    <ul className="folder-tree">
      {data.map((item) => (
        <li key={item.name} onClick={() => onSelect(item)}>
          <div className="tree-node">
            <i className={`fa fa-${item.type}`} />
            <span>{item.name}</span>
          </div>
          {item.children && <FolderTree data={item.children} onSelect={onSelect} />}
        </li>
      ))}
    </ul>
  );
}

// CodeEditor component
function CodeEditor({ code, setCode, fileName }) {
  const handleCodeChange = (e) => {
    setCode(e.target.value);
  };

  return (
    <div className="code-editor-container">
      <div className="line-numbers">
        {code.split("\n").map((line, index) => (
          <div key={index}>{index + 1}</div>
        ))}
      </div>
      <textarea
        className="code-input"
        value={code}
        onChange={handleCodeChange}
        placeholder="// Type your code here"
      />
    </div>
  );
}  

// function CodeEditor() {
//   const [code, setCode] = useState('// Type your code here');
//   const [consoleMessage, setConsoleMessage] = useState('');

//   function runCode() {
//     // Code execution logic here
//     setConsoleMessage('Hello, world!');
//   }

//   return (
//     <div className="container">
//       <div className="sidebar">
//         <FolderTree />
//       </div>
//       <div className="main">
//         <div className="editor">
//           <div className="line-numbers">
//             {code.split('\n').map((_, index) => (
//               <span key={index}>{index + 1}</span>
//             ))}
//           </div>
//           <pre>
//             <code
//               contentEditable="true"
//               spellCheck="false"
//               onInput={(event) => setCode(event.target.textContent)}
//             >
//               {code}
//             </code>
//           </pre>
//         </div>
//         <div className="console">
//           <h3>Console</h3>
//           <div className="console-output">{consoleMessage}</div>
//           <button onClick={runCode}>Run Code</button>
//         </div>
//       </div>
//     </div>
//   );
// }

export default CodeEditor;
