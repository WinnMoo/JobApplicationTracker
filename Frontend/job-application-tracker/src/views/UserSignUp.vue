<template>
  <div class="UserSignUp">
    <v-container>
      <v-alert v-model="errorAlert" type="error" dismissible dense>{{ formErrorMessage }}</v-alert>
      <v-alert v-model="popup" type="success">{{ popupText }}</v-alert>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="6" md="10">
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Sign Up</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-row>
                <v-col>
                  <v-text-field
                    v-model="emailAddress"
                    label="Email*"
                    clearable
                    prepend-icon="mdi-email"
                  ></v-text-field>
                </v-col>
                <v-col>
                  <v-text-field
                    v-model="firstName"
                    label="First Name*"
                    clearable
                    prepend-icon="mdi-account"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <v-text-field
                    v-model="password"
                    :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                    :type="show1 ? 'text' : 'password'"
                    name="input-10-1"
                    label="Password*"
                    hint="At least 8 characters"
                    counter
                    @click:append="show1 = !show1"
                    prepend-icon="mdi-lock"
                  ></v-text-field>
                </v-col>
                <v-col>
                  <v-text-field
                    :v-model="verifyPassword"
                    :append-icon="show4 ? 'mdi-eye' : 'mdi-eye-off'"
                    :type="show4 ? 'text' : 'password'"
                    name="input-10-2"
                    label="Verify Password*"
                    hint="At least 8 characters"
                    counter
                    @click:append="show4 = !show4"
                    prepend-icon="mdi-lock"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <v-select
                    v-model="securityQuestion1"
                    :items="securityQuestions"
                    :rules="[v => !!v || 'Item is required']"
                    label="Security Question 1*"
                    required
                    prepend-icon="mdi-shield-account"
                  ></v-select>
                </v-col>
                <v-col>
                  <v-text-field
                    v-model="securityAnswer1"
                    label="Security Answer 1*"
                    clearable
                    prepend-icon="mdi-forum"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <v-select
                    v-model="securityQuestion2"
                    :items="securityQuestions"
                    :rules="[v => !!v || 'Item is required']"
                    label="Security Question 2*"
                    required
                    prepend-icon="mdi-shield-account"
                  ></v-select>
                </v-col>
                <v-col>
                  <v-text-field
                    v-model="securityAnswer2"
                    label="Security Answer 2*"
                    clearable
                    prepend-icon="mdi-forum"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <v-select
                    v-model="securityQuestion3"
                    :items="securityQuestions"
                    :rules="[v => !!v || 'Item is required']"
                    label="Security Question 3*"
                    required
                    prepend-icon="mdi-shield-account"
                  ></v-select>
                </v-col>
                <v-col>
                  <v-text-field
                    v-model="securityAnswer3"
                    label="Security Answer 3*"
                    clearable
                    prepend-icon="mdi-forum"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <PrivacyPolicyDialog></PrivacyPolicyDialog>
                </v-col>
                <v-col>
                  <v-checkbox
                    v-model="checkbox"
                    :rules="[v => !!v || 'You must agree to continue!']"
                    label="Do you agree to our privacy policy?"
                    required
                    :style="{ right: '15%', transform: 'translateX(+15%)' }"
                  ></v-checkbox>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <a id="LoginLink" @click="goToLogin">Already Have an Account?</a>
              <v-spacer></v-spacer>
              <v-btn :disabled="!valid" color="primary" class="mr-4" @click="signUp">Sign Up</v-btn>
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
import PrivacyPolicyDialog from "@/components/UserSignUpDialogs/PrivacyPolicyDialog.vue";

export default {
  name: "UserSignUp",
  components: {
    PrivacyPolicyDialog
  },
  data() {
    return {
      popup: false,
      popupText: "",
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
  watch: {},
  methods: {
    goToLogin: function() {
      this.$router.push("/login");
    },
    validate: function() {
      this.$refs.form.validate();
    },
    reset: function() {
      this.$refs.form.reset();
    },
    resetValidation: function() {
      this.$refs.form.resetValidation();
    },
    signUp: function() {
      if (
        this.securityQuestion1 == null ||
        this.securityQuestion2 == null ||
        this.securityQuestion3 == null
      ) {
        this.formErrorMessage = "Please choose 3 security questions.";
        this.errorAlert = true;
      }
      if (
        this.securityQuestion1 === this.securityQuestion2 ||
        this.securityQuestion1 === this.securityQuestion3 ||
        this.securityQuestion2 == this.securityQuestion3
      ) {
        this.formErrorMessage = "Please choose 3 different security questions.";
        this.errorAlert = true;
      }
      if (
        this.securityQuestion1 == null ||
        this.securityQuestion2 == null ||
        this.securityQuestion3 == null
      ) {
        this.formErrorMessage = "Please fill in your security answers.";
        this.errorAlert = true;
      }
      var strongRegex = new RegExp(
        "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*.,;'+=-])(?=.{8,})"
      );
      if (!strongRegex.test(this.password)) {
        this.formErrorMessage =
          "Password must contain 1 uppercase character, 1 lowercase character, 1 number, 1 special character and be at least 8 characters long.";
        this.errorAlert = true;
      }
      if (!this.password.localeCompare(this.verifyPassword)) {
        this.formErrorMessage = "Passwords do not match";
        this.errorAlert = true;
      }
      if (this.firstName === null) {
        this.formErrorMessage = "Please enter your first name.";
        this.errorAlert = true;
      }
      if (this.emailAddress === null) {
        this.formErrorMessage = "Please enter an email address.";
        this.errorAlert = true;
      }
      if (this.checkbox == false) {
        this.formErrorMessage = "Please read and accept our privacy policy";
        this.errorAlert = true;
      }
      if (!this.errorAlert) {
        axios({
          method: "POST",
          url: `${apiURL}/account/` + "create",
          data: {
            EmailAddress: this.$data.emailAddress,
            FirstName: this.$data.firstName,
            Password: this.$data.password,
            SecurityQuestion1: this.$data.securityQuestion1,
            SecurityQuestion2: this.$data.securityQuestion2,
            SecurityQuestion3: this.$data.securityQuestion3,
            SecurityAnswer1: this.$data.securityAnswer1,
            SecurityAnswer2: this.$data.securityAnswer2,
            SecurityAnswer3: this.$data.securityAnswer3
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            this.popup = true;
            this.popupText =
              response.data +
              ". You will be redirected to the login page in 5 seconds.";
            setTimeout(() => {
              this.$router.push("/login");
            }, 5000);
          })
          .catch(e => {
            this.formErrorMessage = e.response.data;
          })
          .finally(() => {
            this.loading = false;
          });
      }
    }
  }
};
</script>
<style>
#LoginLink {
  font-size: 14px;
  padding: 8px;
}
</style>
