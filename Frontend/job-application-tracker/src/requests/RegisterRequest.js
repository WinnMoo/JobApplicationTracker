export default class RegisterRequest{
  constructor(emailAddress, firstName, password, securityQuestion1, securityQuestion2, securityQuestion3, securityAnswer1, securityAnswer2, securityAnswer3) {
    this.emailAddress = emailAddress;
    this.firstName = firstName;
    this.password = password;
    this.securityQuestion1 = securityQuestion1;
    this.securityQuestion2 = securityQuestion2;
    this.securityQuestion3 = securityQuestion3;
    this.securityAnswer1 = securityAnswer1;
    this.securityAnswer2 = securityAnswer2;
    this.securityAnswer3 = securityAnswer3;
  }
}