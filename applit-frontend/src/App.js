import React from "react";
import Editor from "./Editor";
import Folder from "./Folder"; 
import CodeEditor from "./Test";

const folder = {
  name: "root",
  content: [
    {
      name: "folder1",
      type: "folder",
      content: [
        {
          name: "file1.txt",
          type: "file",
        },
        {
          name: "folder2",
          type: "folder",
          content: [
            {
              name: "file2.txt",
              type: "file",
            },
          ],
        },
      ],
    },
    {
      name: "file3.txt",
      type: "file",
    },
  ],
};

const App = () => {
  return (
    <div>
      <Editor /> 
      {/* <CodeEditor /> */}
      {/* <Folder folder={folder} /> */}
    </div>
  );
};

export default App;

