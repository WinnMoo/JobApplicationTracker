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
              <v-col cols="12" sm="6" md="6">
                <v-text-field label="City" v-model="city" hint="E.g. Irvine"></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <v-text-field label="State" v-model="state" hint="E.g. California"></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" sm="12" md="12">
                <v-textarea
                  label="Description"
                  v-model="description"
                ></v-textarea>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="12" sm="12" md="12">
                <v-text-field label="Link to job posting" v-model="jobPostingUrl" required></v-text-field>
                <div v-if="this.parseError" id="parseError">Unable to parse job posting</div>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-progress-circular v-if="loading" indeterminate color="primary"></v-progress-circular>
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
  name: "AddJobApplicationDialog",
  props: {},
  data() {
    return {
      addDialog: false,
      companyName: null,
      jobTitle: null,
      description: null,
      city: null,
      state: null,
      location: null,
      jobPostingUrl: null,
      loading: false,
      error: false,
      parseError: false,
      errorLoadingJobPostingMessage: null,
      rules: {
        required: value => !!value || "Required."
      }
    };
  },
  watch: {
    jobPostingUrl: function() {
      this.debouncedParseJobPosting();
    }
  },
  created: function() {
    this.debouncedParseJobPosting = _.debounce(this.ParseJobPosting, 500);
  },
  methods: {
    ParseJobPosting: function() {
      if (this.jobPostingUrl != null) {
        this.loading = true;
        axios({
          method: "POST",
          url: `${apiURL}/jobposting/` + "parse",
          data: {
            UrlToParse: this.jobPostingUrl
          },
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Credentials": true
          }
        })
          .then(response => {
            console.log(response.data);
            this.companyName = response.data.company;
            this.description = response.data.description;
            this.jobTitle = response.data.jobTitle;
            this.state = response.data.state;
            this.city = response.data.city;
            this.jobPostingUrl = response.data.url;
          })
          .catch(e => {
            this.parseError = true;
            this.errorLoadingJobPostingMessage = e.response.data;
          })
          .finally(() => {
            this.loading = false;
          });
      }
    },
    closeAddDialog: function() {
      this.$refs.form.reset();
      this.jobPostingUrl = null;
      this.parseError = false;
      this.addDialog = false;
    },
    addJobApplication: function() {
      if (
        this.companyName != null &&
        this.jobTitle != null
      ) {
        this.$emit(
          "addJobApplication",
          this.companyName,
          this.jobTitle,
          this.description,
          this.jobPostingUrl,
          this.city,
          this.state
        );
        this.closeAddDialog();
      }
    }
  }
};
</script>
<style>
#parseError {
  color: red;
}
</style>