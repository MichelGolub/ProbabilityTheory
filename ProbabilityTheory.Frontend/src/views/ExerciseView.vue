<template>
<h4>{{ $t('exercise.description') }}</h4>
<a @click="askForRemoveExercise" v-if="authorized" style="cursor: pointer">{{ $t('exercise.delete' )}}</a>
<div class="row">
    <ExerciseDescription 
        :id="exercise.id" 
        :description="exercise.description" 
        v-on:descriptionChanged="updateExerciseDescription"/>
</div>

<h4>{{ $t('solution') }}</h4>
<div class="row">
    <a v-if="authorized" style="cursor: pointer" @click="addStep">{{ $t('step.add') }}</a>
</div>
<div v-for="(step, index) in exercise.steps" :key="step.id" class="row">
    <StepItem :number="index+1" :step="step"/>
</div>

<ExerciseDeleteModal ref="exerciseDeleteModal"/>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

import ExerciseDeleteModal from '@/components/Exercise/ExerciseDeleteModal.vue'

import ExerciseDescription from '@/components/Exercise/ExerciseDescription.vue'
import StepItem from '@/components/Exercise/StepItem.vue'

import DeleteIcon from 'icons/AlertCircle'
import EditIcon from 'icons/Pencil'

export default {
    name: 'PageView',
    components: {
        ExerciseDeleteModal,
        ExerciseDescription,
        StepItem,
        DeleteIcon, EditIcon
    },
    data() {
        return {
            
        }
    },
    computed: {
        ...mapGetters([
            'exercise'
        ]),
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        exerciseId() { return this.$route.params.exerciseId }
    },
    methods: {
        ...mapActions([
            'getExercise',
            'updateExercise',
            'createStep'
        ]),
        askForRemovePage() {
            this.$refs.exerciseDeleteModal.open(this.exerciseId)
        },
        addStep() {
            let exerciseId = this.exerciseId
            this.createStep({exerciseId})
        },
        load() {
            let languageId = this.languageId
            let exerciseId = this.exerciseId
            return new Promise(() => {
                this.getExercise({languageId, exerciseId})
            })
        },
        updateExerciseDescription(description) {
            let payload = {}
            payload.languageId = this.languageId
            payload.exerciseId = this.exerciseId
            payload.description = description
            this.updateExercise(payload)
        }
    },
    mounted() {
        this.load()
    },
    watch: {
        languageId() {
            this.load()
        },
        exerciseId(newExerciseId) {
            if(!newExerciseId) {
                return
            }
            this.load()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "exercise": {
            "edit": "edit",
            "delete": "delete this exercise",
            "description": "Description"
        },
        "solution": "Solution",
        "step": {
            "add": "Add step"
        }
    },
    "ukr": {
        "exercise": {
            "edit": "редагувати",
            "delete": "видалити це завдання",
            "description": "Опис"
        },
        "solution": "Рішення",
        "step": {
            "add": "Додати крок"
        }
    }
}
</i18n>