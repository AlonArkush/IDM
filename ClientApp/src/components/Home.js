import React, { Component } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <Form id="createForm">
      <Form.Group className="mb-3" controlId="formUserName">
        <Form.Label>User Name</Form.Label>
        <Form.Control type="text" placeholder="Enter User Name" required/>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formFirstName">
        <Form.Label>First Name</Form.Label>
        <Form.Control type="text" placeholder="First Name" required/>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formSurname">
        <Form.Label>Surname</Form.Label>
        <Form.Control type="text" placeholder="Surname" required/>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formOrganization">
        <Form.Label>Organization</Form.Label>
        <Form.Control type="text" placeholder="Organization" required/>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formTelephone">
        <Form.Label>Telephone</Form.Label>
        <Form.Control type="tel" placeholder="Telephone" required/>
      </Form.Group>
      <Button variant="primary" type="button" onClick={this.sendForm}>
        Submit
      </Button>
    </Form>
    );
    
  }
  sendForm() {
    console.log(document.forms["createForm"]["formFirstName"].value)
    let formObj = new Object()
    formObj.FirstName = document.forms["createForm"]["formFirstName"].value
    formObj.SurName = document.forms["createForm"]["formSurname"].value
    formObj.Username = document.forms["createForm"]["formUserName"].value
    formObj.Phone = document.forms["createForm"]["formTelephone"].value
    formObj.Organization = document.forms["createForm"]["formOrganization"].value
    console.log(formObj)
    fetch("aduser", {
      contentType: "text/json",
      method: "POST",
      body: JSON.stringify(formObj)
    })
  }
}