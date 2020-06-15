<template>
  <v-card class="mx-auto text-center" dark>
    <v-card-title class="display-1 font-weight-thin justify-center">Statistics About Your Applications</v-card-title>
    <v-tabs v-model="tab" background-color="primary" dark grow>
      <v-tab v-for="item in items" :key="item.tab">
        {{ item.tab }}
      </v-tab>
      <v-tab-item>
        <v-card dark>
          <v-card-title class="justify-center">
            <div class="display-1 font-weight-thin">Job Applications within the past month</div>
          </v-card-title>
          <v-card-text>
              <v-sparkline
                :fill="fill"
                :gradient="gradient"
                :line-width="widthSparkline"
                :padding="padding"
                :smooth="radius || false"
                :value="value"
                auto-draw
              >
                <template v-slot:label="item">
                  {{ item.value }}
                </template>
              </v-sparkline>
          </v-card-text>
        </v-card>
      </v-tab-item>
      <v-tab-item>
        <v-card dark>
          <v-card-title class="justify-center">
            <div class="display-1 font-weight-thin">Job Applications within the past month</div>
          </v-card-title>
          <v-card-text>
              <vue-funnel-graph :width="width" :height="height" :labels="labels"
              :values="values" :colors="colors" :direction="direction"
              :gradient-direction="gradientDirection"
              :animated="true" 
              ></vue-funnel-graph>
          </v-card-text>
        </v-card>
      </v-tab-item>
    </v-tabs>
  </v-card>
</template>

<script>
import { VueFunnelGraph } from 'vue-funnel-graph-js';
export default {
  name: "JobApplicationStats",
  components: {
      VueFunnelGraph
  },
  data() {
    return {
      fill: true,
      padding: 8,
      radius: 10,
      value: [0, 2, 5, 9, 5, 10, 3, 5, 0, 0, 1, 8, 2, 9, 0],
      widthSparkline: 2,
      gradients: [
        ['#222'],
        ['#42b3f4'],
        ['red', 'orange', 'yellow'],
        ['purple', 'violet'],
        ['#00c6ff', '#F0F', '#FF0'],
        ['#f72047', '#ffd200', '#1feaea'],
      ],
      tab: null,
      items: [
        { tab: 'Applications Over Time', content: 'Tab 1 Content' },
        { tab: 'Applications Statuses', content: 'Tab 2 Content' }
      ],
      labels: ['Applications Sent', 'Phone Screens', 'Interviews', 'Offers'],
      values: [
      // with the given Labels and SubLabels here's what the values represent:
      // 
      // Direct, Social, Ads  
      //    |      |     |  
      //    v      v     v
          [3000], // Segments of "Applications Sent" from top to bottom
          [1500], // Segments of "Phone Screen"
          [800],   // Segments of "Interview"
          [10] // Segments of "Offer"
      ],
      direction: 'horizontal',
      gradientDirection: 'horizontal',
      height: 300,
      width: 800
    }
  }
}
</script>
