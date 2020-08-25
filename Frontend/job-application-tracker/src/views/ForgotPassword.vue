<template>
  <div class="ForgotPassword">
    <v-container>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="6" md="10">
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Forgot Password</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              Forgot your password? No problem!
              Enter your email below, and we'll send you a link to reset your password.
              <v-row>
                <v-col>
                  <v-text-field
                    v-model="emailAddress"
                    label="Email"
                    clearable
                    prepend-icon="mdi-email"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn :disabled="!isEmailAddressFilled" color="primary">Send Reset Password Link</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>
<script>
import axios from "axios";
import { apiURL } from "@/const.js";
export default {
  name: "ForgotPassword",
  data() {
    return {
      emailAddress: "",
      isEmailAddressFilled: false
    };
  },
  watch: {
    emailAddress: function() {
      if (this.emailAddress == null || this.emailAddress.length == 0) {
        this.isEmailAddressFilled = false;
      } else {
        this.isEmailAddressFilled = true;
      }
    }
  },
  methods: {
    SendPasswordResetLink() {
      if (this.emailAddress != null) {
        axios({
          method: "POST",
          url: `${apiURL}/account/` + "login",
          data: {
            EmailAddress: this.$data.emailAddress
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            this.popup = response;
            this.popupText = "You are logged in.";
            localStorage.setItem = ("isLoggedIn", true);
          })
          .catch(e => {
            this.formErrorMessage = e.response.data;
          })
          .finally(() => {
            this.loading = false;
            localStorage.setItem = ("isLoggedIn", false);
          });
      }
    }
  }
};
</script>