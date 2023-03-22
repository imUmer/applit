import React, { useEffect, useState }  from "react";
import "./Cards.css";
import axios from "axios";
import { Route } from 'react-router-dom';
import { useNavigate  } from 'react-router-dom';
import Loading from "../loading/Loading";

const Cards = ({ items }) => {
  
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);

  function lauch() {
    navigate('/editor');
 
  } 
 
    async function fetchData() {
      const response = await axios.get('https://jsonplaceholder.typicode.com/todos/1');
      console.log(response.data);
      setLoading(false);
      navigate('/editor');
    } 


  const lauchButton = async (name, event) => {
    const containerPrams = new FormData();
    containerPrams.append("image", name);
    containerPrams.append("name", name + "3");
    console.log(typeof containerPrams);
    
    setLoading(true);
    await axios
      .post("http://localhost:5054/api/Run", containerPrams, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then(function (response) {
        navigate('/editor');
        setLoading(false);
      //  <Route path="/editor" component={<Editor />}/>
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
            <form onSubmit={() => fetchData()}>
              <input type="submit" value="Gooo" />
            </form>
            {loading ? 
              <Loading />
             : (
              <button onClick={fetchData}>Next Page</button>
            )}
            <form onSubmit={() => lauchButton(item.imagename)}>
              <input type="submit" value={item.buttonText} />
            </form>
            <button onClick={lauch}>Go</button>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Cards;
