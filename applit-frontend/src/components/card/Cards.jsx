import React from "react";
import "./Cards.css";
import axios from "axios";

const Cards = ({ items }) => {
  const lauchButton = async (name, event) => {
    const containerPrams = new FormData();
    containerPrams.append("image", name);
    containerPrams.append("name", name + "3");
    console.log(typeof containerPrams);
    await axios
      .post("http://localhost:5054/api/Run", containerPrams, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then(function (response) {
        navigator("./Editor");
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
  };
  return (
    <div className="cards-container">
      {items.map((item, index) => (
        <div key={index} className="card">
          <img src={item.image} alt={`Card ${index}`} />
          <div className="card-info">
            <h3>{item.title}</h3>
            <p>{item.description}</p>
            <form onSubmit={() => lauchButton(item.imagename)}>
              <input type="submit" value={item.buttonText} />
            </form>
            {/* <button onSubmit={()=>lauchButton(item.imagename)}>{item.buttonText}</button> */}
          </div>
        </div>
      ))}
    </div>
  );
};

export default Cards;
