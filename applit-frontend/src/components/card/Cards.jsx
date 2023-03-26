import React, { useEffect, useState }  from "react";
import "./Cards.css";
import axios from "axios";
import { Route } from 'react-router-dom';
import { useLocation,useNavigate  } from 'react-router-dom';
import Loading from "../loading/Loading";

const Cards = ({ items }) => {
  
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const location = useLocation();

  function lauch() {
    navigate('/editor');
 
  } 
 
    async function fetchData() {
      const response = await axios.get('https://jsonplaceholder.typicode.com/todos/1');
      console.log(response.data);
      setLoading(false);
      navigate('/editor');
    } 

    
async function launchFunction(name) {
  try {
    setLoading(true);
    const containerPrams = new FormData();
    containerPrams.append("image", name);
    containerPrams.append("name", name + "1111");
    // Perform some asynchronous operation, e.g. fetching data from an API
    const response = await axios.post("http://localhost:5054/api/Run", containerPrams) 
    if (response) { 
      // Once the data has been fetched, navigate to a different route
      navigate('/editor');
      setLoading(false);
    }
  } catch (error) {
    // Handle any errors that may occur during the asynchronous operation
    console.error(error);
  }
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
      .then( async (response)=> {
        
        navigate('/editor');
        // setLoading(false);
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
            {/* <form onSubmit={() => fetchData()}>
              <input type="submit" value="Gooo" />
            </form>
            <form onSubmit={() => launchFunction(item.imagename)}>
              {loading && <Loading/> }<input type="submit" value={item.buttonText} />
            </form> */}
            <button onClick={() => launchFunction((item.imagename))}>{!loading ? <>Go</> :loading && <Loading text="please wait..."/> }</button>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Cards;
