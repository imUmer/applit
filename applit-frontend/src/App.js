import React from "react";
import Navbar from "./components/navbar/Navbar";
import Hero from "./components/hero/Hero";
import Editor from "./components/editor/Editor";
import Folder from "./components/folder/Folder";
import CodeEditor from "./Test";
import Cards from "./components/card/Cards";
import './App.css';

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
const items = [
  {
    image:
      "https://th.bing.com/th/id/OIP.PWZbnxYsW3OPHoU2DO0UqQHaE8?pid=ImgDet&rs=1",
    title: "Python",
    imagename: "python",
    description:
      "Run your python code/Scripts in online IDE. Click and Launch your envoirnment",
    buttonText: "Launch Python",
  },
  {
    image:
      "https://th.bing.com/th/id/R.bc71c1c1c50551a1d65e7b529ea29d08?rik=EU42gCFH4J%2bBZA&riu=http%3a%2f%2fwww.goodworklabs.com%2fwp-content%2fuploads%2f2016%2f10%2freactjs.png&ehk=qvQ5EVoUnJZ7k5L347zsU3f87YTckr1iQBzKdwXJd0w%3d&risl=&pid=ImgRaw&r=0",
    title: "React Js",
    imagename: "bayesimpact/react-base",
    description:
      "Make you react js project in online IDE. Click and Launch your envoirnment",
    buttonText: "Launch React",
  },
  {
    image:
      "https://th.bing.com/th/id/OIP.eAbbuLEe_-03B2eeywx5DAHaDt?pid=ImgDet&rs=1",
    title: "C++",
    imagename: "gcc",
    description:
      "Pratice you codind in online C++ IDE. Click and Launch your envoirnment",
    buttonText: "Launch C++",
  }, 
];

const images = [
  "https://via.placeholder.com/800x400?text=Image%201",
  "https://via.placeholder.com/800x400?text=Image%202",
  "https://via.placeholder.com/800x400?text=Image%203",
];
const App = () => {
 
  return (
    <div className="App">
      <Navbar />
      <Cards items={items} />  
    </div>
  );
};

export default App;
