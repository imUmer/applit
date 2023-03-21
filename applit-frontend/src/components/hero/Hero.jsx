import React, { useState } from "react";
import "./Hero.css";

const Hero = ({ images }) => {
  const [activeIndex, setActiveIndex] = useState(0);

  const handlePrev = () => {
    setActiveIndex((activeIndex - 1 + images.length) % images.length);
  };

  const handleNext = () => {
    setActiveIndex((activeIndex + 1) % images.length);
  };

  return (
    <div className="carousel-container">
      <div className="carousel-image-container">
        {images.map((image, index) => (
          <img
            key={index}
            src={image}
            alt={`Carousel Image ${index}`}
            className={`carousel-image ${index === activeIndex && "active"}`}
          />
        ))}
      </div>
      <button className="carousel-prev" onClick={handlePrev}>
        &lt;
      </button>
      <button className="carousel-next" onClick={handleNext}>
        &gt;
      </button>
    </div>
  );
};

export default Hero;
