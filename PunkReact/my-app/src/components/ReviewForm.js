import { getByDisplayValue } from '@testing-library/react';
import React, { useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { v4 as uuidv4 } from 'uuid';

const ReviewForm = (props) => {
  const [review, setReview] = useState({
    username: props.review ? props.review.username : '',
    rating: props.review ? props.review.rating : '',
    comments: props.review ? props.review.comments : '',
    beer_id: props.beerId ? props.beerId : ''
  });

  const [ username, setUserName ] = useState('');
  const [ rating, setRating ] = useState('');
  const [ comments, setComments ] = useState('');
  const [ beer_id, setBeerId ] = useState('');

  const [errorMsg, setErrorMsg] = useState('');
  const emailRegex = "^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

  const handleOnSubmit = (event) => {
    event.preventDefault();
    const values = [username, rating, comments];
    let errorMsg = '';

  const submitReviewToAPI = (review,bid) => {
    console.log(review);
    fetch(props.apiConnectionString+'postRating/'+bid, {
      method: 'POST',
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        "username" : review.username,
        "rating" : review.rating,
        "comments" : review.comments
      })
    })
  }

    const allFieldsFilled = values.every((field) => {
      const value = `${field}`.trim();
      return value !== '' && value !== '0';
    });

    if (allFieldsFilled) {
      const review = {
        username,
        rating,
        comments
      };
      submitReviewToAPI(review,props.beerId);
    } else {
      errorMsg = 'Please fill out all the fields.';
    }
    setErrorMsg(errorMsg);
  };

  const handleInputChange = (event) => {
    switch (event.target.name) {
      case 'beer_id':         
        fetch(props.apiConnectionString+`beers/?beer_name=${event.target.value}`)
        .then((res) => res.json())
        .then((result) => setBeerId(event.target.value));
        break;
      case 'username':
        if (event.target.value !== '') {
          setUserName(event.target.value);
        }
        break;
      case 'comments':
        if (event.target.value !== '') {
          setComments(event.target.value);
        }
        break;
      case 'rating':
        if (event.target.value !== '') {
          setRating(event.target.value);
        }
        break;

    }
  };

  if( props.beerId && props.beerId !== -1 ){
    console.log("Have beer id:"+props.beerId);
    return (
      <div className="main-form card">
        {errorMsg && <p className="errorMsg">{errorMsg}</p>}
        <Form onSubmit={handleOnSubmit}>
          <Form.Group controlId="username">
            <Form.Label>Email</Form.Label>
            <Form.Control
              className="input-control"
              type="text"
              name="username"
              value={username}
              placeholder="email username"
              onChange={handleInputChange}
            />
          </Form.Group>
          <Form.Group controlId="rating">
            <Form.Label>Rating</Form.Label>
            <Form.Control
              className="input-control"
              type="number"
              name="rating"
              value={rating}
              placeholder="Enter rating (0-5)"
              onChange={handleInputChange}
            />
          </Form.Group>
          <Form.Group controlId="comments">
            <Form.Label>Review</Form.Label>
            <Form.Control
              className="input-control"
              type="textarea"
              name="comments"
              value={comments}
              placeholder="Enter full review"
              onChange={handleInputChange}
            />
          </Form.Group>
          <Button variant="primary" type="submit" className="submit-btn">
            Submit
          </Button>
        </Form>
      </div>
    );
  }
  else{
    console.log("No beer id:"+props.beerId);
    return (<br />);
  }
}

export default ReviewForm;