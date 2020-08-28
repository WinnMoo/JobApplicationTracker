<template>
  <v-dialog v-model="addDialog" persistent max-width="600px" ref="dialog">
    <template v-slot:activator="{ on }">
      <v-btn class="mx-2" fab dark color="indigo" fixed left bottom v-on="on">
        <v-icon dark>mdi-plus</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-form ref="form">
        <v-card-title>
          <span class="headline">Add New Job Application</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="6">
                <v-text-field
                  :rules="[rules.required]"
                  label="Company Name*"
                  v-model="companyName"
                  hint="E.g. Microsoft"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <v-text-field
                  :rules="[rules.required]"
                  label="Job Title*"
                  v-model="jobTitle"
                  hint="E.g. Software Engineer"
                  required
                ></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" sm="12" md="12">
                <v-textarea
                  :rules="[rules.required]"
                  label="Description*"
                  v-model="description"
                  required
                ></v-textarea>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" sm="12" md="12">
                <v-text-field
                  :rules="[rules.required]"
                  label="Link to job posting"
                  v-model="jobPostingUrl"
                  required
                ></v-text-field>
                <div v-if="this.error" id="parseError">
                  Unable to parse job posting
                </div>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-progress-circular
            v-if="loading"
            indeterminate
            color="primary"
          ></v-progress-circular>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="closeAddDialog()">Close</v-btn>
          <v-btn color="blue darken-1" text @click="addJobApplication()">Save</v-btn>
        </v-card-actions>
      </v-form>
    </v-card>
  </v-dialog>
</template>

<script src="https://cdn.jsdelivr.net/npm/lodash@4.13.1/lodash.min.js"></script>
<script>
import axios from "axios";
import lodash from "lodash";
import { apiURL } from "@/const.js";
export default {
  name: "add-job-application-dialog",
  props: {},
  data() {
    return {
      addDialog: false,
      companyName: null,
      jobTitle: null,
      description: null,
      location: null,
      jobPostingUrl: null,
      loading: false,
      error: false,
      errorLoadingJobPostingMessage: null,
      rules: {
        required: value => !!value || "Required."
      }
    };
  },
  watch: {
    jobPostingUrl: function(){
      console.log("watching");
      this.debouncedParseJobPosting();
    }
  },
  created: function () {
    this.debouncedParseJobPosting = _.debounce(this.ParseJobPosting, 500);
  },
  methods: {
    ParseJobPosting: function () {
      console.log("parsing");
      this.loading = true;
      axios({
      method: "POST",
        url: `${apiURL}/jobposting/` + "parse",
        data: {
          urlToParse: this.jobPostingUrl
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Credentials": true
        }
      })
        .then(response => {
          this.popup = true;
          this.companyName = response.data.CompanyName;
          this.description = response.data.description;
          this.jobTitle = response.data.jobTitle;
        })
        .catch(e => {
          this.error = true;
          this.errorLoadingJobPostingMessage = e.response.data;
        })
        .finally(() => {
          this.loading = false;
        });
    },
    closeAddDialog: function () {
      this.$refs.form.reset();
      this.addDialog = false;
    },
    addJobApplication: function() {
      this.$emit(
        "addJobApplication",
        this.companyName,
        this.jobTitle,
        this.description
      );
      this.$refs.form.reset();
      this.addDialog = false;
    }
  }
};
</script>
<style>
  #parseError{
    color:red;
  }
</style>