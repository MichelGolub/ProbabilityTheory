<template>
    <div>
        <div class="row">
            <a @click="addExercise" v-if="authorized" style="cursor: pointer">
                {{ $t('exercise.add') }}
            </a>
        </div>

        <div class="row">
            <router-view></router-view>
        </div>

        <div class="divider"></div>
        <div class="row">
            <div class="center-align">
                <ul class="pagination">
                    <li v-for="(exercise, index) in exercises" :key="index"
                    :class="{ active: $route.params['exerciseId'] === exercise.id }"
                    class="waves-effect">
                        <router-link :to="{ name: 'exercise', params: { exerciseId: exercise.id } }">
                            {{ index + 1 }}
                        </router-link>
                    </li>
                </ul>
            </div>
        </div>

        <div class="row">
            <h5 v-show="!exercises?.length">{{ $t('exercise.noExercises') }}</h5>
        </div>
    </div>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

export default {
    name: 'ExercisesView',
    computed: {
        ...mapGetters([
            'chapter'
        ]),
        exercises() { return this.chapter.exercises },
        exercisesLength() { return this.exercises.length },
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        id() { return this.$route.params.id }
    },
    methods: {
        ...mapActions([
            'createExercise'
        ]),
        addExercise() {
            let chapterId = this.id
            this.createExercise({chapterId})
            .then((resp) => {
                this.$router.push({ name: 'exercise', params: { exerciseId: resp.data } })
            })
        },
        moveToFirstPageIfExist() {
            if (this.exercises.length) {
                let exercise = this.exercises[0]
                let route = this.$router.resolve({
                    name: 'exercise',
                    params: { exerciseId: exercise.id }
                })
                this.$router.push(route)
            }
        }
    },
    mounted() {
        this.moveToFirstPageIfExist()
    },
    watch: {
        exercisesLength() {
            this.moveToFirstPageIfExist()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "exercise": {
            "add": "Add new exercise",
            "noExercises": "There is no exercises"
        }
    },
    "ukr": {
        "exercise": {
            "add": "Створити нову сторінку",
            "noExercises": "Немає вправ"
        }
    }
}
</i18n>