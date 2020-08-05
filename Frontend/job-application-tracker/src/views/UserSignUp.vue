<template>
  <div class="UserSignUp">
    <v-alert v-model="errorAlert" type="error" dismissible dense>
      {{ formErrorMessage }}
    </v-alert>
    <v-form>
      <v-container fluid>
        <v-row align="center" justify="center">
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="emailAddress"
              label="Email*"
              clearable
            ></v-text-field>
          </v-col>

          <v-col cols="12" sm="6">
            <v-text-field
              v-model="firstName"
              label="First Name*"
              clearable
            ></v-text-field>
          </v-col>

          <v-col cols="12" sm="6">
            <v-text-field
              v-model="password"
              :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
              :type="show1 ? 'text' : 'password'"
              name="input-10-1"
              label="Password*"
              hint="At least 8 characters"
              counter
              @click:append="show1 = !show1"
            ></v-text-field>
          </v-col>

          <v-col cols="12" sm="6">
            <v-text-field
              :v-model="verifyPassword"
              :append-icon="show4 ? 'mdi-eye' : 'mdi-eye-off'"
              :type="show4 ? 'text' : 'password'"
              name="input-10-2"
              label="Verify Password*"
              hint="At least 8 characters"
              counter
              @click:append="show4 = !show4"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-select
              v-model="securityQuestion1"
              :items="securityQuestions"
              :rules="[v => !!v || 'Item is required']"
              label="Security Question 1*"
              required
            ></v-select>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="securityAnswer1"
              label="Security Answer 1*"
              clearable
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-select
              v-model="securityQuestion2"
              :items="securityQuestions"
              :rules="[v => !!v || 'Item is required']"
              label="Security Question 2*"
              required
            ></v-select>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="securityAnswer2"
              label="Security Answer 2*"
              clearable
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-select
              v-model="securityQuestion3"
              :items="securityQuestions"
              :rules="[v => !!v || 'Item is required']"
              label="Security Question 3*"
              required
            ></v-select>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field v-model="securityAnswer3" label="Security Answer 3*" clearable></v-text-field>
          </v-col>

          <v-col cols="12" sm="6">
            Fields with * are required.
          </v-col>

          <v-col cols="12" sm="6">
            <v-btn color="red lighten-2" dark @click="privacyPolicyDialog = true">
              Privacy Policy
            </v-btn>
          </v-col>

          <v-col cols="12" sm="3">
            <v-checkbox v-model="checkbox"
              :rules="[v => !!v || 'You must agree to continue!']"
              label="Do you agree to our privacy policy?"
              required
            ></v-checkbox>
          </v-col>
          
          <v-col cols="12" sm="1">
            <v-btn :disabled="!valid" color="primary" class="mr-4" @click="signUp" >
              Sign Up
            </v-btn>
          </v-col>
        </v-row>
      </v-container>
    </v-form>
    <v-dialog v-model="privacyPolicyDialog" width="500">
        <v-card>
          <v-card-title class="headline blue lighten-2" primary-title>
            Privacy Policy
          </v-card-title>

          <v-card-text>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" text @click="privacyPolicyDialog = false">
              I accept
            </v-btn>
          </v-card-actions>
        </v-card>
    </v-dialog>
  </div>
</template>

<script>
import axios from 'axios'
import { apiURL } from '@/const.js';
export default {
  name: "UserSignUp",
  data() {
    return {
      privacyPolicyDialog: false,
      valid: true,
      show1: false,
      show2: true,
      show3: false,
      show4: false,
      emailAddress: null,
      firstName: null,
      password: null,
      verifyPassword: null,
      securityQuestion1: null,
      securityQuestion2: null,
      securityQuestion3: null,
      securityAnswer1: null,
      securityAnswer2: null,
      securityAnswer3: null,
      checkbox: false,
      formErrorMessage: null,
      errorAlert: false,
      rules: {
        required: value => !!value || "Required.",
        min: v => v.length >= 8 || "Min 8 characters"
      },
      securityQuestions: [
        "What is your favorite color?",
        "What is the make of your first car?",
        "What is the name of your first pet?",
        "What is the name of your first grade teacher?",
        "What high school did you attend?"
      ]
    };
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    reset() {
      this.$refs.form.reset();
    },
    resetValidation() {
      this.$refs.form.resetValidation();
    },
    signUp() {
      if(this.securityQuestion1 == null || this.securityQuestion2 == null || this.securityQuestion3 == null){
        this.formErrorMessage = "Please choose 3 security questions.";
        this.errorAlert = true;
      }
      if(this.securityQuestion1 === this.securityQuestion2 || 
         this.securityQuestion1 === this.securityQuestion3 || 
         this.securityQuestion2 == this.securityQuestion3){
        this.formErrorMessage = "Please choose 3 different security questions.";
        this.errorAlert = true;
      }
      if(this.securityQuestion1 == null || this.securityQuestion2 == null || this.securityQuestion3 == null){
        this.formErrorMessage = "Please fill in your security answers.";
        this.errorAlert = true;
      }
      var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})");
      if(!strongRegex.test(this.password)){
        this.formErrorMessage = "Password must contain 1 uppercase character, 1 lowercase character, 1 number, 1 special character and be at least 8 characters long.";
        this.errorAlert = true;
      }
      if(!this.password.localeCompare(this.verifyPassword)){
        this.formErrorMessage = "Passwords do not match"
        this.errorAlert = true;
      }
      if(this.firstName === null){
        this.formErrorMessage = "Please enter an email address."
        this.errorAlert = true;
      }
      if(this.emailAddress === null){
        this.formErrorMessage = "Please enter an email address."
        this.errorAlert = true;
      }
      if(this.checkbox == false){
        this.formErrorMessage = "Please read and accept our privacy policy"
        this.errorAlert = true;
      }
      axios({
        method: 'POST',
        url: `${apiURL}/account/` + 'create',
        data: {
          EmailAddress: this.$data.emailAddress,
          FirstName: this.$data.firstName,
          Password: this.$data.password,
          SecurityQuestion1: this.$data.securityQuestion1,
          SecurityQuestion2: this.$data.securityQuestion2,
          SecurityQuestion3: this.$data.securityQuestion3,
          SecurityAnswer1: this.$data.securityAnswer1,
          SecurityAnswer2: this.$data.securityAnswer2,
          SecurityAnswer3: this.$data.securityAnswer3},
        headers: {
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Credentials': true
        }
        })
        .then(response => {
          this.popup = response;
          this.popupText = "Account Successfully Created";
        })
        .catch(e => { this.formErrorMessage = e.response.data })
        .finally(() => {
          this.loading = false;
        })
    }
  }
};
</script>
