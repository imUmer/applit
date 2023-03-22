import React from "react";
import "./Loading.css"; 

const Loading = ({ items }) => {
 
  return (
    <div class="container">
      <div class="loading">
        <div class="spinner"></div>
      </div>
    </div>
  );
};

export default Loading;
