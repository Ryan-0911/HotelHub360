<script setup>
import { ref, reactive } from 'vue'
const fullpageRef = ref()
const options = reactive({
  licenseKey: 'OPEN-SOURCE-GPLV3-LICENSE',
  // 是否顯示導航，默認為 false
  navigation: true,
  // 為每个 section 設置背景色
  sectionsColor: [
    '#42A5F5',
    '#26C6DA',
    '#66BB6A',
    '#CDDC39',
    '#FFEB3B',
    '#FF9800',
    '#FF5722',
    '#795548',
    '#EA80FC'
  ]
})
const upFn = () => {
  // 向上滾動一頁
  fullpageRef?.value?.api.moveSectionUp()
}
const leftFn = () => {
  // 向左滾動一頁
  fullpageRef?.value?.api.moveSlideLeft()
}
const rightFn = () => {
  // 向右滾動一頁
  fullpageRef?.value?.api.moveSlideRight()
}
const next = () => {
  // 向下滾動一頁
  fullpageRef?.value?.api.moveSectionDown()
}
const last = () => {
  // 向下滾動一頁
  console.log(fullpageRef?.value?.api)
  fullpageRef?.value?.api.moveSectionUp()
}
const moveToFn = () => {
  // 向 第幾頁 滾動
  // fullpageRef?.value?.api.moveTo(3,0);
  fullpageRef?.value?.api.moveTo(3, 2) // 滾動向 直第三頁 横第二頁
  // fullpage_api.moveTo('firstSlide', 2);
  // fullpage_api.moveTo(3, 0);
  // fullpage_api.moveTo(3);
}
</script>

<template>
  <div>
    <full-page :options="options" ref="fullpageRef">
      <div class="section">
        <div style="text-align: center">
          1⃣
          <button @click="moveToFn">moveToFn</button>
          <button @click="last">最後一頁</button>
        </div>
      </div>
      <div class="section">
        <div style="text-align: center">
          2⃣️
          <button @click="next">下一頁</button>
          <button @click="upFn">上一頁</button>
        </div>
      </div>
      <div class="section">
        <div class="slide">
          <div style="height: 100vh">
            1
            <button @click="rightFn">右</button>
            3⃣️
          </div>
        </div>
        <div class="slide">
          <div style="height: 100vh">
            <div style="text-align: center">
              <button @click="leftFn">左</button>
              <button @click="rightFn">右</button>
              3
            </div>
          </div>
        </div>
        <div class="slide">
          <div style="height: 100vh">
            2
            <button @click="leftFn">左</button>
            3⃣️
          </div>
        </div>
      </div>
      <div class="section">
        <div class="box3">
          <div style="height: 100vh">4⃣️</div>
        </div>
      </div>
    </full-page>
    <div></div>
  </div>
</template>
