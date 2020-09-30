<template>
  <v-container>
    <v-alert v-model="popup" dismissible type="success">{{ popupText }}</v-alert>
    <v-alert v-model="errorPopup" dismissible type="error">{{ errorMessage}}</v-alert>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="6" md="8">
        <v-card class="elevation-12">
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>Login</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-text-field label="Email" v-model="emailAddress" prepend-icon="mdi-account"></v-text-field>
            <v-text-field
              label="Password"
              v-model="password"
              prepend-icon="mdi-lock"
              :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
              :type="show ? 'text' : 'password'"
              @click:append="show = !show"
            ></v-text-field>
            <a @click="GoToForgotPassword">Forgot Password?</a>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" @click="Login">Login</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import axios from "axios";
import { apiURL } from "@/const.js";

export default {
  name: "Login",
  data() {
    return {
      popup: false,
      popupText: null,
      errorPopup: false,
      errorMessage: null,
      emailAddress: null,
      password: null,
      show: false
    };
  },
  methods: {
    Login() {
      if (this.emailAddress == null || this.password == null) {
        alert("Please fill in the form completely");
      } else {
        axios({
          method: "POST",
          url: `${apiURL}/session/` + "login",
          data: {
            EmailAddress: this.$data.emailAddress,
            Password: this.$data.password
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            this.popup = true;
            this.popupText =
              "You are logged in. You will be redirected to you job applications page in 3 seconds.";
            this.$store.dispatch("logIn");
            this.$store.dispatch("setEmailAddress", this.emailAddress);
            this.$store.dispatch("setFirstName", response.data.firstName)
            localStorage.setItem("jwtToken", response.data.JWTToken);
            setTimeout(() => {
              this.$router.push("/jobapplications");
            }, 3000);
            this.$forceUpdate;
          })
          .catch(e => {
            this.formErrorMessage = e.response.data;
          });
      }
    },
    GoToForgotPassword() {
      this.$router.push("/forgotpassword");
    }
  }
};
</script>