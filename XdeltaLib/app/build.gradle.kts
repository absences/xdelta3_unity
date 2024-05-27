plugins {
    id ("com.android.library")
}

android {

    externalNativeBuild {
        ndkBuild {
            path ("src/main/jni/Android.mk")
        }
    }

    namespace = "com.gygame.lib"
    compileSdk = 34

    defaultConfig {
        //applicationId = "com.gygame.lib"
        minSdk = 24
        //targetSdk = 34
        //versionCode = 1
        //versionName = "1.0"

        //estInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"

    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }
    sourceSets {

        getByName("main") {
            jniLibs.srcDirs("libs")
        }
    }
}

dependencies {

    implementation(libs.appcompat)
    implementation(libs.material)
   // testImplementation(libs.junit)
   // androidTestImplementation(libs.ext.junit)
   //androidTestImplementation(libs.espresso.core)
}