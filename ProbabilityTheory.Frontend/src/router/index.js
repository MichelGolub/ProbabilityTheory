import { createRouter, createWebHistory } from 'vue-router'

import { vuexOidcCreateRouterMiddleware } from 'vuex-oidc'
import store from '@/store'

const routes = [
  {
    path: '/',
    name: 'home',
    redirect: { name: 'chapters' }
  },
  {
    path: '/chapters',
    name: 'chapters',
    component: () => import('@/views/ChaptersView.vue')
  },
  {
    path: '/chapters/:id',
    name: 'chapter',
    component: () => import('@/views/ChapterView.vue'),
    children: [
      {
        path: '/chapters/:id/pages',
        name: 'pages',
        component: () => import('@/views/PagesView.vue'),
        children: [
          {
            path: '/chapters/:id/pages/:pageId',
            name: 'page',
            component: () => import('@/views/PageView.vue')
          }
        ]
      },
      {
        path: '/chapters/:id/questions',
        name: 'questions',
        component: () => import('@/views/QuestionsView.vue'),
        children: [
          {
            path: '/chapters/:id/questions/:questionId',
            name: 'question',
            component: () => import('@/views/QuestionView.vue')
          }
        ]
      },
      {
        path: '/chapters/:id/exercises',
        name: 'exercises',
        component: () => import('@/views/ExercisesView.vue'),
        children: [
          {
            path: '/chapters/:id/exercises/:exerciseId',
            name: 'exercise',
            component: () => import('@/views/ExerciseView.vue')
          }
        ]
      }
    ]
  },
  {
    path: '/terms',
    name: 'terms',
    component: () => import('@/views/TermsView.vue')
  },
  {
    path: '/terms/:id',
    name: 'term',
    component: () => import('@/views/TermView.vue')
  },/*
  {
    path: '/signin-oidc',
    name: 'oidcSignInCallback',
    component: () => import('@/views/OidcCallback.vue')
  },
  {
    path: '/signout-oidc',
    name: 'oidcSignOutCallback',
    redirect: { name: 'home' }
  }*/
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

//router.beforeEach(vuexOidcCreateRouterMiddleware(store))

export default router
