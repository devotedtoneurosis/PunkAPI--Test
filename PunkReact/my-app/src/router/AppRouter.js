import React from 'react';
import { BrowserRouter as Router, Routes ,Route, Link } from "react-router-dom";
import ReviewList from '../components/ReviewList';
import ReviewForm from '../components/ReviewForm';

const apiString = 'http://localhost:64937/';

const AppRouter = () => {
  return (
    <Router>
    <nav className="navbar navbar-expand navbar-dark bg-dark">x
        PunkAPI Review Site
    </nav>

    <div className="container mt-3">
      <Routes>
        <Route exact path="/" element={<ReviewList apiConnectionString={apiString}/>} />
      </Routes>
    </div>



  </Router>
  );
};

export default AppRouter;