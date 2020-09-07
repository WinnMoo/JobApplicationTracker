<template>
  <div class="ResetPassword">
    <v-container>
      <v-alert v-model="errorAlert" type="error" dismissible dense>{{ errorMessage }}</v-alert>
      <v-alert v-model="popup" type="success">{{ popupText }}</v-alert>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="6" md="10">
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Reset Password</v-toolbar-title>
            </v-toolbar>
            <v-progress-linear :active="loading" :indeterminate="loading" height="5" striped></v-progress-linear>
            <v-card-text>
              <v-row>
                <v-col>
                  <div id="SecurityQuestion">{{ securityQuestions.securityQuestion1 }}</div>
                </v-col>
                <v-col>
                  <v-text-field v-model="securityAnswer1" label="Security Answer 1" clearable></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <div id="SecurityQuestion">{{ securityQuestions.securityQuestion2 }}</div>
                </v-col>
                <v-col>
                  <v-text-field v-model="securityAnswer2" label="Security Answer 2" clearable></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <div id="SecurityQuestion">{{ securityQuestions.securityQuestion3 }}</div>
                </v-col>
                <v-col>
                  <v-text-field v-model="securityAnswer3" label="Security Answer 3" clearable></v-text-field>
                </v-col>
              </v-row>
              <v-row v-if="areSecurityAnswersCorrect">
                <v-col cols="12">
                  <v-text-field
                    v-model="newPassword"
                    label="New Password"
                    clearable
                    prepend-inner-icon="mdi-lock"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                :disabled="!areAnswersFilled"
                color="primary"
                @click="buttonClick"
              >{{ buttonText }}</v-btn>
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
      canResetPassword: false,
      buttonText: "Check Security Answers",
      loading: true,
      popup: false,
      popupText: null,
      errorMessage: null,
      errorAlert: false,
      resetToken: this.$route.params.id,
      securityQuestions: {
        securityQuestion1: "What is the name of your first pet?",
        securityQuestion2: "Lorem epsum asdfasdfasdfasdf",
        securityQuestion3: "Lorem epsum asdfasdfasdfasdf"
      },
      newPassword: "",
      securityAnswer1: "",
      securityAnswer2: "",
      securityAnswer3: "",
      areAnswersFilled: false,
      areSecurityAnswersCorrect: false
    };
  },
  created: {
    // TODO: add api call to fetch security answers
  },
  watch: {
    securityAnswer1: function() {
      this.CheckIfAnswersAreFilled();
    },
    securityAnswer2: function() {
      this.CheckIfAnswersAreFilled();
    },
    securityAnswer3: function() {
      this.CheckIfAnswersAreFilled();
    }
  },
  methods: {
    CheckIfAnswersAreFilled: function() {
      if (
        this.securityAnswer1 != null &&
        this.securityAnswer1.length > 1 &&
        this.securityAnswer2 != null &&
        this.securityAnswer2.length > 1 &&
        this.securityAnswer3 != null &&
        this.securityAnswer3.length > 1
      ) {
        this.areAnswersFilled = true;
      } else {
        this.areAnswersFilled = false;
      }
    },
    buttonClick: function() {
      if (!this.canResetPassword) {
        this.CheckAnswers();
      } else {
        this.ResetPassword();
      }
    },
    CheckAnswers: function() {
      axios({
        method: "POST",
        url: `${apiURL}/account/` + "checkSecurityAnswers",
        data: {
          PasswordResetToken: "asdf",
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
          this.popupText = response.data;
          this.buttonText = "Reset Password";
          this.canResetPassword = true;
        })
        .catch(e => {
          this.formErrorMessage = e.response.data;
        })
        .finally(() => {
          this.loading = false;
        });
    },
    ResetPassword: function() {
      axios({
        method: "POST",
        url: `${apiURL}/account/` + "resetpassword",
        data: {
          PasswordResetToken: "asdf",
          NewPassword: this.$data.newPassword
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
};
</script>
<style>
#SecurityQuestion {
  line-height: 70px;
  font-size: 18px;
}
</style>
