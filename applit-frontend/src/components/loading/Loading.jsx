// import React from "react";
import "./Loading.css"; 

// const Loading = () => {
 
//   return (
//     <div class="container">
//       <div class="loading">
//         <div class="spinner"></div>
//       </div>
//     </div>
//   );
// };

// export default Loading;

import React from 'react'; 

const Loading = ({ text = 'Loading...' }) => {
  return (
    <div class="loader-circle">
  <p class="loader-content">LOADING</p>
  <div class="loader-line-mask">
    <div class="loader-line"></div>
  </div>
</div>
  );
};

export default Loading;
