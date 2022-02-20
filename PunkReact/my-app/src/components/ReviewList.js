import { React, useState } from 'react';
import { TextField, Button } from '@mui/material/';
import ReviewForm from './ReviewForm';


const ReviewList = (props) => {
    const [ beerName, setBeerName ] = useState('');
    const [ beerReviews, setBeerReviews ] = useState('');
    const [ beerReviewIndex, setBeerReviewIndex ] = useState('');

    const handleBeerNameInput = e => {
        setBeerName(e.target.value);
      };

    const handleBeerReviewIndexUpdate = (bId) => {
      setBeerReviewIndex(bId);
    };

    const getBeerReviews = e => {
        console.log(props.apiConnectionString+`?q=${beerName}`);
        console.log(beerReviews);
        fetch(props.apiConnectionString+`?q=${beerName}`)
        .then((res) => res.json())
        .then((result) => setBeerReviews(result));
    }
    
    return (
    <div>
      <div className="col-md-6" style={{float: 'left'}}>
        <h3>Search Reviews</h3>
        <TextField 
            id="outlined-name"
            label="Beer Name"
            variant="outlined" 
            value={beerName}
            onChange={handleBeerNameInput}/>
        <Button 
            variant="contained"
            onClick={getBeerReviews}>
            Search Reviews
        </Button>
 
        <ul className="list-group">
          {beerReviews &&
            beerReviews.map((beerReview, index) => (
              <li key={index} style={{listStyle: 'none', marginBottom: 1 + 'em'}} >
                <div className="card">
                <h5>Beer: {beerReview.name}</h5>
                <p>Description: <i>{beerReview.description}</i></p>
                <Button 
                  variant="contained"
                  onClick={() => handleBeerReviewIndexUpdate(beerReview.id)}
                  style={{textAlign: 'right', marginBottom: 0}}>
                  Add Review
                </Button>
                Reviews:
                <ul className="list-group">
                {beerReview.userRatings &&
                      beerReview.userRatings.map((userRating, rInd) => (
                          <li key={rInd} style={{paddingLeft: 2 + 'em'}}>
                              <div className="card">
                              {userRating.rating}/5<br></br>
                              <i>{userRating.comments}</i><br></br>
                              <p style={{fontWeight: 100}}>{userRating.username}</p>
                              </div>
                          </li>
                      ))}
                  </ul>
                </div>
              </li>
            ))}
        </ul>
      </div>
      <div className="col-md-6" style={{float: 'right'}}>
          <ReviewForm apiConnectionString={props.apiConnectionString} beerId={beerReviewIndex} />
      </div>
    </div>

    );
}

export default ReviewList;