<template>
  <v-card class="mx-auto">
    <v-container>
      <v-row>
        <v-list-item>
          <v-list-item-content>
            <v-col>
              <v-list-item-title class="display-1 text--primary">{{ jobApplication.company }}</v-list-item-title>
            </v-col>
            <v-col class="text-right">
              <v-list-item-title>
                <v-icon>mdi-map-marker</v-icon>
                {{ jobApplication.location }}
              </v-list-item-title>
            </v-col>
            <v-col cols="10">
              <v-list-item-subtitle>{{ jobApplication.jobTitle }}</v-list-item-subtitle>
            </v-col>
            <v-col cols="2" class="text-right">
              <v-chip v-if="jobApplication.status == 1" color="green" text-color="white">Accepted</v-chip>
              <v-chip v-if="jobApplication.status == -1" color="error" text-color="white">Rejected</v-chip>
              <v-chip
                v-if="jobApplication.status == 0"
                color="orange"
                text-color="white"
              >In Progress</v-chip>
            </v-col>
            <v-col>
              <v-row justify="space-between">
                <v-col cols="10">
                  <v-card-text>
                    <div class="text--primary">{{ jobApplication.description }}</div>
                  </v-card-text>
                </v-col>
                <v-row class="flex-column ma-0 fill-height" justify="center">
                  <v-col class="text-right">
                    <v-col class="px-0">
                      <v-btn
                        icon
                        color="primary"
                        @click="updateDialog = true;
                    indexToUpdate = index;
                    companyName = jobApplications[index].company;
                    jobTitle = jobApplications[index].jobTitle;
                    description = jobApplications[index].description"
                      >
                        <v-icon>mdi-pencil</v-icon>
                      </v-btn>
                    </v-col>
                    <v-col class="px-0">
                      <v-btn icon color="error" @click="deleteDialog = true; indexToDelete = index">
                        <v-icon>mdi-trash-can</v-icon>
                      </v-btn>
                    </v-col>
                  </v-col>
                </v-row>
              </v-row>
            </v-col>
          </v-list-item-content>
        </v-list-item>
      </v-row>
    </v-container>
  </v-card>
</template>

<script>
export default {
  name: "job-application-card",
  props:['jobApplications'],
  data() {
    return {};
  },
  methods: {}
};
</script>