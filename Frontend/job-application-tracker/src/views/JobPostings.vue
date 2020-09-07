<template>
    <div class="JobPostings" style="text-align:center;" fill-height fluid>
    <v-container>
      <v-row align="center" justify="center">
        <v-col cols="12">
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Job Postings</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-row>
                <v-text-field
                prepend-icon="mdi-magnify"
                clearable
                label="Search for jobs">

                </v-text-field>
                <v-btn
                color="blue"
                dark>
                  Search
                </v-btn>
              </v-row>
              
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
    </div>
</template>

<script>
export default {
  name: "jobpostings",
  data() {
    return {
      searchTerms: null
    };
  },
  methods: {

  }
}
</script>