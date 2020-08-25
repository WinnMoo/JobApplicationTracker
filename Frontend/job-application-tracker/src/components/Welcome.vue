<template>
  <v-container>
    <v-row class="text-center">
      <v-col class="mb-4">
        <h1 class="display-2 font-weight-bold mb-3">
          Welcome to Job Application Tracker
        </h1>

        <p class="subheading font-weight-regular">
          Job Application Tracker allows you to keep track of your job applications in a beautiful, organized environment.
          <br />To get started, please sign up for an account, or if have an account, please login.
        </p>
        <a href="/signup"> Create an account</a>
        <br />
        <a href="/login"> Login </a>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
    name: "Welcome",
    data() {
        return {
        }
    },
    methods: {
    }
}
</script>