import React, { Component } from 'react';


export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { adusers: [], loading: true };
  }

  componentDidMount() {
    this.populateADUserData();
  }

  static renderADUsersTable(adusers) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>First Name</th>
            <th>Surname</th>
            <th>Username</th>
            <th>phone</th>
            <th>Organization</th>
          </tr>
        </thead>
        <tbody>
          {adusers.map(aduser =>
            <tr key={aduser.firstName}>
              <td>{aduser.firstName}</td>
              <td>{aduser.surname}</td>
              <td>{aduser.userName}</td>
              <td>{aduser.phone}</td>
              <td>{aduser.organization}</td>
            </tr>
          )}
        </tbody>
      </table>
      
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderADUsersTable(this.state.adusers);

    return (
      <div>
        {contents}
      </div>    
    );
  }

  async populateADUserData() {
    const response = await fetch('aduser');
    const data = await response.json();
    this.setState({ adusers: data, loading: false });
  }
}
