<template>
  <v-form>
    <v-container fluid>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="6">
          <v-text-field
            v-model="emailAddress"
            label="Email"
            clearable
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="6">
          <v-text-field
            v-model="firstName"
            label="First Name"
            clearable
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="6">
          <v-text-field
            v-model="password"
            :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
            :rules="[rules.required, rules.min]"
            :type="show1 ? 'text' : 'password'"
            name="input-10-1"
            label="Password"
            hint="At least 8 characters"
            counter
            @click:append="show1 = !show1"
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="6">
          <v-text-field
            :v-model="verifyPassword"
            :append-icon="show4 ? 'mdi-eye' : 'mdi-eye-off'"
            :rules="[rules.required, rules.min, rules.emailMatch]"
            :type="show4 ? 'text' : 'password'"
            name="input-10-2"
            label="Verify Password"
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
            label="Security Question 1"
            required
          ></v-select>
        </v-col>
        <v-col cols="12" sm="6">
          <v-text-field
            v-model="securityAnswer1"
            label="Security Answer 1"
            clearable
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="6">
          <v-select
            v-model="securityQuestion2"
            :items="securityQuestions"
            :rules="[v => !!v || 'Item is required']"
            label="Security Question 2"
            required
          ></v-select>
        </v-col>
        <v-col cols="12" sm="6">
          <v-text-field
            v-model="securityAnswer2"
            label="Security Answer 2"
            clearable
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="6">
          <v-select
            v-model="securityQuestion3"
            :items="securityQuestions"
            :rules="[v => !!v || 'Item is required']"
            label="Security Question 3"
            required
          ></v-select>
        </v-col>
        <v-col cols="12" sm="6">
          <v-text-field
            v-model="securityAnswer3"
            label="Security Answer 3"
            clearable
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="3">
        <v-checkbox
          v-model="checkbox"
          :rules="[v => !!v || 'You must agree to continue!']"
          label="Do you agree?"
          required
        ></v-checkbox>
        </v-col>
        
        <v-col cols="12" sm="1">
        <v-btn
          :disabled="!valid"
          color="primary"
          class="mr-4"
          @click="validate"
        >
          Sign Up
        </v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script>
export default {
  name: "UserSignUp",
  data() {
    return {
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
    }
  }
};
</script>
